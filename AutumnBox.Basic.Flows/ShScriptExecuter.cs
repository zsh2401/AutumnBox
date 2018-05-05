/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/29 20:06:13
** filename: ShScriptExecuter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.ActivityManager;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;
using AutumnBox.Support.Log;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 脚本执行器参数
    /// </summary>
    public class ShScriptExecuterArgs : FlowArgs
    {
        /// <summary>
        /// 是否修复Android O下ADB自动关闭的问题(开启网络调试)
        /// </summary>
        public bool FixAndroidOAdb { get; set; } = false;
    }
    /// <summary>
    /// Shell脚本执行器
    /// </summary>
    public abstract class ShScriptExecuter : FunctionFlow<ShScriptExecuterArgs, AdvanceResult>
    {
        /// <summary>
        /// 修复失败
        /// </summary>
        public event EventHandler<Exception> FixFailed;
        /// <summary>
        /// 参数
        /// </summary>
        protected ShScriptExecuterArgs Args { get; private set; }
        /// <summary>
        /// 包名
        /// </summary>
        protected virtual string AppPackageName { get; } = null;
        /// <summary>
        /// 主Activity名
        /// </summary>
        protected virtual string AppActivity
        {
            get
            {
                return new PackageInfo(Args.Serial, AppPackageName).MainActivity;
            }
        }
        /// <summary>
        /// 脚本路径
        /// </summary>
        protected abstract string ScriptPath { get; }
        /// <summary>
        /// 启动界面后的延迟毫秒数
        /// </summary>
        protected virtual int Delay { get; } = 3000;
        /// <summary>
        /// 执行结果
        /// </summary>
        protected AdvanceOutput _result;
        /// <summary>
        /// 工具箱
        /// </summary>
        protected ToolKit<ShScriptExecuterArgs> _tooKit;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="moduleArgs"></param>
        protected override void Initialization(ShScriptExecuterArgs moduleArgs)
        {
            base.Initialization(moduleArgs);
            Args = moduleArgs;
        }
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<ShScriptExecuterArgs> toolKit)
        {
            _tooKit = toolKit;
            var ob = new AdvanceOutputBuilder();
            if (AppPackageName != null && AppActivity != null)
            {
                Logger.Info(this, Activity.Start(toolKit.Args.Serial, AppPackageName, AppActivity));
                Thread.Sleep(Delay);
            }
            int retCode = 0;
            using (AndroidShell shell = new AndroidShell(toolKit.Args.DevBasicInfo.Serial))
            {
                shell.Connect();
                ob.Register(shell);
                retCode = shell.SafetyInput($"sh {ScriptPath}").GetExitCode();
            }
            ob.ExitCode = retCode;
            _result = ob.Result;
            return _result;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = _result.GetExitCode();
            result.ResultType = result.ExitCode == 0 ? ResultType.Successful : ResultType.Unsuccessful;
            if (_tooKit.Args.FixAndroidOAdb &&//如果需要在激活后开启ADB
                (new DeviceBuildPropGetter(_tooKit.Args.DevBasicInfo.Serial).
                GetAndroidVersion() >= new Version("8.0.0")//并且安卓版本是8.0
                ) &&
                _result.GetExitCode() == 0
                )
            {
                TryFixAndroidAdb();
            }
        }
        /// <summary>
        /// 尝试开启网络调试
        /// </summary>
        protected async virtual void TryFixAndroidAdb()
        {
            try
            {
                await Task.Run(() =>
                {
                    uint port = 5555;
                    Thread.Sleep(3000);

                    var opener = new NetDebuggingOpener();
                    opener.Init(new NetDebuggingOpenerArgs() { DevBasicInfo = _tooKit.Args.DevBasicInfo, Port = port });
                    var openerResult = opener.Run();


                    if (openerResult.ExitCode == 0)//如果开启成功
                    {
                        Thread.Sleep(3000);//稍等一会儿
                        IPAddress ip = new DeviceSoftwareInfoGetter(_tooKit.Args.Serial).GetLocationIP();//获取设备IP

                        //连接到该设备
                        var connecter = new NetDeviceConnecter();
                        connecter.Init(new NetDeviceConnecterArgs() { IPEndPoint = new IPEndPoint(ip, (int)port) });
                        connecter.Run();
                    }

                    Logger.Info(this, "Fix android o adb successful....");
                });
            }
            catch (Exception e)
            {
                FixFailed(this, e);
                Logger.Warn(this, "Fix android o adb failed....", e);
            }
        }
    }
}
