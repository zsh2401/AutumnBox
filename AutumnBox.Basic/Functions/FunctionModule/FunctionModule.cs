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
namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Devices;
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Functions.Event;
    using AutumnBox.Basic.Functions.Interface;
    using AutumnBox.Basic.Util;
    using System;
    using System.Diagnostics;
    using System.Threading;
    /// <summary>
    /// 各种功能模块的父类
    /// </summary>
    public abstract class FunctionModule : BaseObject, IDisposable
    {
        #region 事件
        /// <summary>
        /// 当功能模块开始执行时发生
        /// </summary>
        internal event StartedEventHandler Started;
        /// <summary>
        /// 完成操作时的事件
        /// </summary>
        internal event FinishedEventHandler Finished;
        /// <summary>
        /// 执行时接收到输出时发生
        /// </summary>
        internal event DataReceivedEventHandler OutReceived;
        /// <summary>
        /// 执行时接收到错误输出时发生
        /// </summary>
        internal event DataReceivedEventHandler ErrorReceived;
        /// <summary>
        /// 低层开始命令进行时发生,可获取PID
        /// </summary>
        internal event ProcessStartedEventHandler ProcessStarted;
        /// <summary>
        /// 如果是被非正常停止的,此值为True
        /// </summary>
        protected internal bool WasFrociblyStop = false;
        #endregion
        #region 保护属性
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
        protected internal DeviceSimpleInfo DevSimpleInfo { get; set; }
        #endregion
        /// <summary>
        /// 判断完成事件是否被绑定
        /// </summary>
        public bool IsFinishEventBound
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
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="info"></param>
        protected FunctionModule(DeviceSimpleInfo info) : this()
        {
            DevSimpleInfo = info;
        }
        /// <summary>
        /// 开始执行函数,但不建议直接调用,建议使用RunningManager进行托管
        /// </summary>
        /// <param name="delayTime">延迟执行的时间,如果不写则立刻开始</param>
        /// <returns></returns>
        internal void RunByRunningManager()
        {
            if (DevSimpleInfo == null) throw new ArgumentNullException();
            new Thread(() => { _Run(); })
            {
                Name = TAG + " MainMethod"
            }.Start();
        }
        internal OutputData FastRun() {
            LogD("Fast Run");
            OutputData o = new OutputData();
            Finished += (s, e) => { o = e.OutputData; };
            _Run();
            return o;
        }
        /// <summary>
        /// 运行过程
        /// </summary>
        private void _Run()
        {
            OnStarted(new StartEventArgs());
            var fullOutput = MainMethod();
            ExecuteResult executeResult = new ExecuteResult
            {

                IsSuccessful = !(WasFrociblyStop),
                OutputData = fullOutput
            };
            HandingOutput(fullOutput, ref executeResult);
            OnFinished(new FinishEventArgs { Result = executeResult });
        }
        #region 主体
        /// <summary>
        /// 准备执行核心功能时,将调用此方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="a"></param>
        protected virtual void OnStarted(StartEventArgs a)
        {
            Logger.D(TAG, "Started");
            Started?.Invoke(this, a);
        }
        /// <summary>
        /// 模块的核心代码,强制要求子类进行实现
        /// </summary>
        protected abstract OutputData MainMethod();
        protected virtual void OnProcessStarted(ProcessStartedEventArgs e)
        {
            LogD($"Process ID {e.PID}");
            ProcessStarted?.Invoke(this, e);
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
        protected virtual void HandingOutput(OutputData output, ref ExecuteResult executeResult)
        {
        }
        /// <summary>
        /// 引发Finished事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="o"></param>
        protected virtual void OnFinished(FinishEventArgs e)
        {
            Logger.D(TAG, "Finished");
            Finished?.Invoke(this, e);
        }
        /// <summary>
        /// 析构!
        /// </summary>
        public void Dispose()
        {
#pragma warning disable CA1063
            Executer.Dispose();
#pragma warning disable CA1063
        }
        #endregion
    }
}
