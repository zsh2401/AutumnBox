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
    using AutumnBox.Basic.Function.Bundles;
    using AutumnBox.Basic.Function.Event;
    using AutumnBox.Support.CstmDebug;
    using AutumnBox.Support.Helper;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    /// <summary>
    /// 各种功能模块的父类
    /// </summary>
    public abstract class FunctionModule : IFunctionModule, ILogSender
    {
        /// <summary>
        /// 核心进程id
        /// </summary>
        private int? CoreProcessPid { get; set; }
        /// <summary>
        /// 模块参数
        /// </summary>
        public ModuleArgs Args
        {
            set
            {
                Create(new BundleForCreate(value));
            }
        }
        /// <summary>
        /// 模块状态
        /// </summary>
        public ModuleStatus Status { get; private set; } = ModuleStatus.Loading;
        public static event FinishedEventHandler AnyFinished;
        /// <summary>
        /// 当功能模块开始执行时发生
        /// </summary>
        public event StartupEventHandler Startup;
        /// <summary>
        /// 完成操作时的事件
        /// </summary>
        public event FinishedEventHandler Finished;
        /// <summary>
        /// 核心进程开始时发生
        /// </summary>
        public event ProcessStartedEventHandler CoreProcessStarted;
        /// <summary>
        /// 执行时接收到任何输出时发生
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;
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

        public string LogTag => "FunctionModule";

        public bool IsShowLog => true;

        /// <summary>
        /// 用于传给MainMethod的工具包
        /// </summary>
        private BundleForTools _toolsBundle;
        /// <summary>
        /// 构造
        /// </summary>
        protected FunctionModule() { }
        /// <summary>
        /// 同步运行
        /// </summary>
        public void Run()
        {
            try
            {
                OnStartup(new StartupEventArgs());
                ExecuteResult executeResult;
                BundleForAnalyzingResult bundleForAnalyzeOutput = null;
                if (Check(_toolsBundle.Args) == CheckResult.OK)
                {
                    Status = ModuleStatus.Running;
                    var fullOutput = MainMethod(_toolsBundle);
                    Status = (Status == ModuleStatus.ForceStoped) ? ModuleStatus.ForceStoped : ModuleStatus.Finished;
                    executeResult = new ExecuteResult(fullOutput);
                    bundleForAnalyzeOutput = new BundleForAnalyzingResult() { Result = executeResult };
                    AnalyzeOutput(bundleForAnalyzeOutput);
                }
                else
                {
                    executeResult = new ExecuteResult(new OutputData())
                    {
                        Level = ResultLevel.Unsuccessful,
                        WasForcblyStop = false,
                    };
                    Status = ModuleStatus.CannontStart;
                }
                OnFinished(new FinishEventArgs
                {
                    Result = executeResult,
                    Other = bundleForAnalyzeOutput?.Other
                });
            }
            catch (Exception e)
            {
                Logger.T("A exception happend !", e);
            }
        }
        /// <summary>
        /// 强制杀死核心进程
        /// </summary>
        public void ForceStop()
        {
            if (CoreProcessPid == null) return;
            Status = ModuleStatus.ForceStoped;
            SystemHelper.KillProcessAndChildrens((int)CoreProcessPid);
        }
        /// <summary>
        /// 析构
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toolsBundle.Executer.Dispose();
                ForceStop();
            }
            GC.SuppressFinalize(this);
        }
        #region 虚方法
        protected virtual void Create(BundleForCreate bundle)
        {
            CExecuter executer = new CExecuter();
            _toolsBundle = new BundleForTools(executer, bundle.Args);
            executer.OutputReceived += (s, e) =>
            {
                OnOutputReceived(e);
            };
            executer.ProcessStarted += (s, e) => { OnProcessStarted(e); };
            Status = ModuleStatus.Ready;
        }
        /// <summary>
        /// 开始运行时发生
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStartup(StartupEventArgs e)
        {
            Task.Run(() =>
            {
                Startup?.Invoke(this, e);
            });
        }
        /// <summary>
        /// 在开始执行前进行检查,如果返回false将使功能模块终止运行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual CheckResult Check(ModuleArgs args) { return CheckResult.OK; }
        /// <summary>
        /// 模块的核心代码,强制要求子类进行实现
        /// </summary>
        protected abstract OutputData MainMethod(BundleForTools toolsBundle);
        /// <summary>
        /// 引发OutputReceived事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnOutputReceived(OutputReceivedEventArgs e)
        {
            Task.Run(() =>
            {
                OutputReceived?.Invoke(this, e);
            });
        }
        /// <summary>
        /// 引发进程开始事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProcessStarted(ProcessStartedEventArgs e)
        {
            Task.Run(() =>
            {
                Logger.D(this, $"{GetType().Name} Process start!  get the pid ->" + e.Pid);
                CoreProcessPid = e.Pid;
                CoreProcessStarted?.Invoke(this, e);
            });
        }
        /// <summary>
        /// 处理输出数据
        /// </summary>
        /// <param name="output"></param>
        /// <param name="result"></param>
        protected virtual void AnalyzeOutput(BundleForAnalyzingResult bundleResult)
        {
            bundleResult.Result.Level = (Status == ModuleStatus.ForceStoped) ? ResultLevel.Unsuccessful : ResultLevel.Successful;
            bundleResult.Result.WasForcblyStop = (Status == ModuleStatus.ForceStoped) ? true : false;
        }
        /// <summary>
        /// 引发Finished事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="o"></param>
        protected virtual void OnFinished(FinishEventArgs e)
        {
            if (e.Result.Level != ResultLevel.Successful)
            {
                Logger.T("finished...but unsuccess maybe the output ->" + e.OutputData);
            }
            if (Finished == null) AnyFinished?.Invoke(this, e);
            else Finished(this, e);
        }
        #endregion
    }
}
