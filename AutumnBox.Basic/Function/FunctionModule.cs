/* =============================================================================*\
*
* Filename: FunctionModule.cs
* Description: 
*
* Version: 1.0
* Created: 9/26/2017 18:25:47(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
/**
 *                             _ooOoo_
 *                            o8888888o
 *                            88" . "88
 *                            (| -_- |)
 *                            O\  =  /O
 *                         ____/`---'\____
 *                       .'  \\|     |//  `.
 *                      /  \\|||  :  |||//  \
 *                     /  _||||| -:- |||||-  \
 *                     |   | \\\  -  /// |   |
 *                     | \_|  ''\---/''  |   |
 *                     \  .-\__  `-`  ___/-. /
 *                   ___`. .'  /--.--\  `. . __
 *                ."" '<  `.___\_<|>_/___.'  >'"".
 *               | | :  `- \`.;`\ _ /`;.`/ - ` : | |
 *               \  \ `-.   \_ __\ /__ _/   .-` /  /
 *          ======`-.____`-.___\_____/___.-`____.-'======
 *                             `=---='
 *          ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
 *                     佛祖保佑        永无BUG
 *            佛曰:
 *                   写字楼里写字间，写字间里程序员；
 *                   程序人员写程序，又拿程序换酒钱。
 *                   酒醒只在网上坐，酒醉还来网下眠；
 *                   酒醉酒醒日复日，网上网下年复年。
 *                   但愿老死电脑间，不愿鞠躬老板前；
 *                   奔驰宝马贵者趣，公交自行程序员。
 *                   别人笑我忒疯癫，我笑自己命太贱；
 *                   不见满街漂亮妹，哪个归得程序员？
*/
namespace AutumnBox.Basic.Function
{
    using AutumnBox.Basic.Devices;
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Function.Args;
    using AutumnBox.Basic.Function.Event;
    using AutumnBox.Basic.Util;
    using AutumnBox.Shared;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using static Debug;
    /// <summary>
    /// 各种功能模块的父类
    /// </summary>
    public abstract class FunctionModule : BaseObject, IDisposable, IFunctionModule
    {
        /// <summary>
        /// 核心进程id
        /// </summary>
        private int CoreProcessPid { get; set; }
        /// <summary>
        /// 源模块参数
        /// </summary>
        public ModuleArgs Args { protected get; set; }
        /// <summary>
        /// 向已绑定的设备执行adb命令
        /// </summary>
        protected readonly Func<string, OutputData> Ae;
        /// <summary>
        /// 向已绑定的设备执行fastboot命令
        /// </summary>
        protected readonly Func<string, OutputData> Fe;
        /// <summary>
        /// 执行器
        /// </summary>
        protected readonly CommandExecuter Executer = new CommandExecuter();
        /// <summary>
        /// 功能模块执行时指定的设备id
        /// </summary>
        protected string DeviceID { get { return DevSimpleInfo.Id; } }
        /// <summary>
        /// 绑定的设备简单信息
        /// </summary>s
        protected internal DeviceBasicInfo DevSimpleInfo { get { return Args.DeviceBasicInfo; } }
        /// <summary>
        /// 模块状态
        /// </summary>
        public ModuleStatus Status { get; private set; } = ModuleStatus.Loading;
        /// <summary>
        /// 当功能模块开始执行时发生
        /// </summary>
        public event StartupEventHandler Startup;
        /// <summary>
        /// 完成操作时的事件
        /// </summary>
        public event FinishedEventHandler Finished;
        /// <summary>
        /// 执行时接收到输出时发生
        /// </summary>
        public event DataReceivedEventHandler OutReceived;
        /// <summary>
        /// 执行时接收到错误输出时发生
        /// </summary>
        public event DataReceivedEventHandler ErrorReceived;
        /// <summary>
        /// 核心进程开始时发生
        /// </summary>
        public event ProcessStartedEventHandler CoreProcessStarted;
        /// <summary>
        /// 判断完成事件是否被绑定
        /// </summary>
        public bool IsFinishedEventRegistered
        {
            get
            {
                return Finished != null ? true : false;
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        protected FunctionModule()
        {
            Ae = (command) =>
            { return Executer.Execute(new Command(DevSimpleInfo, command)); };
            Fe = (command) =>
            { return Executer.Execute(new Command(DevSimpleInfo, command, ExeType.Fastboot)); };
            Executer.OutputDataReceived += (s, e) => { OnOutReceived(e); };
            Executer.ErrorDataReceived += (s, e) => { OnErrorReceived(e); };
            Executer.ProcessStarted += (s, e) => { OnProcessStarted(e); };
            TAG = GetType().Name;
            Status = ModuleStatus.WaitingToRun;
        }
        public void Run()
        {
            OnStartup(new StartupEventArgs());
            Status = ModuleStatus.Running;
            AnalyzeArgs(Args);
            var fullOutput = MainMethod();
            var executeResult = new ExecuteResult(fullOutput)
            {
                Level = (Status == ModuleStatus.ForceStoped) ? ResultLevel.Unsuccessful : ResultLevel.Successful,
            };
            AnalyzeOutput(ref executeResult);
            Status = (Status == ModuleStatus.ForceStoped) ? ModuleStatus.ForceStoped : ModuleStatus.Finished;
            OnFinished(new FinishEventArgs { Result = executeResult });
        }

        /// <summary>
        /// 强制杀死核心进程
        /// </summary>
        public void ForceStop()
        {
            SystemHelper.KillProcessAndChildrens(CoreProcessPid);
            Status = ModuleStatus.ForceStoped;
        }
        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
#pragma warning disable CA1063
            Executer.Dispose();
#pragma warning disable CA1063
            ForceStop();
        }
        #region 虚方法
        /// <summary>
        /// 开始运行时发生
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStartup(StartupEventArgs e)
        {
            Startup?.Invoke(this, e);
        }
        /// <summary>
        /// 处理参数
        /// </summary>
        /// <param name="args"></param>
        protected virtual void AnalyzeArgs(ModuleArgs args) { }
        /// <summary>
        /// 模块的核心代码,强制要求子类进行实现
        /// </summary>
        protected abstract OutputData MainMethod();
        /// <summary>
        /// 引发进程开始事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProcessStarted(ProcessStartedEventArgs e)
        {
            CoreProcessPid = e.PID;
            CoreProcessStarted?.Invoke(this, e);
        }
        /// <summary>
        /// 引发OutReceived事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnOutReceived(DataReceivedEventArgs e)
        {
            OutReceived?.Invoke(this, e);
        }
        /// <summary>
        /// 引发ErrorReceived事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnErrorReceived(DataReceivedEventArgs e)
        {
            ErrorReceived?.Invoke(this, e);
        }
        /// <summary>
        /// 处理输出数据
        /// </summary>
        /// <param name="output"></param>
        /// <param name="result"></param>
        protected virtual void AnalyzeOutput(ref ExecuteResult executeResult) { }
        /// <summary>
        /// 引发Finished事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="o"></param>
        protected virtual void OnFinished(FinishEventArgs e)
        {
            Finished?.Invoke(this, e);
        }
        #endregion
    }
}
