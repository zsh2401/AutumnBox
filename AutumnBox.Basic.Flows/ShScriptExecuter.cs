/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/29 20:06:13
** filename: ShScriptExecuter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Support.CstmDebug;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class ShScriptExecuterArgs : FlowArgs
    {
        public bool FixAndroidOAdb { get; set; } = false;
    }
    public abstract class ShScriptExecuter : FunctionFlow<ShScriptExecuterArgs, AdvanceResult>
    {
        public event EventHandler<Exception> FixFailed;
        protected ShScriptExecuterArgs Args { get; private set; }
        protected virtual string AppPackageName { get; } = null;
        protected virtual string AppActivity
        {
            get
            {
                return new PackageInfo(Args.Serial, AppPackageName).MainActivity;
            }
        }
        protected abstract string ScriptPath { get; }
        protected virtual int Delay { get; } = 3000;
        protected CommandExecuterResult _result;
        protected ToolKit<ShScriptExecuterArgs> _tooKit;
        protected override void Initialization(ShScriptExecuterArgs moduleArgs)
        {
            base.Initialization(moduleArgs);
            Args = moduleArgs;
        }
        protected override Output MainMethod(ToolKit<ShScriptExecuterArgs> toolKit)
        {
            _tooKit = toolKit;
            if (AppPackageName != null && AppActivity != null)
            {
                FunctionModuleProxy.Create<ActivityLauncher>(new ActivityLaunchArgs(toolKit.Args.DevBasicInfo)
                { PackageName = AppPackageName, ActivityName = AppActivity }).SyncRun();
                Thread.Sleep(Delay);
            }
            _result = toolKit.Executer.QuicklyShell(_tooKit.Args.DevBasicInfo.Serial, $"sh {ScriptPath}");
            return _result.Output;
        }
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = _result.ExitCode;
            result.ResultType = result.ExitCode == 0 ? ResultType.Successful : ResultType.Unsuccessful;
            if (_tooKit.Args.FixAndroidOAdb &&//如果需要在激活后开启ADB
                (new DeviceBuildPropGetter(_tooKit.Args.DevBasicInfo.Serial).
                GetAndroidVersion() >= new Version("8.0.0")//并且安卓版本是8.0
                ) &&
                _result.ExitCode == 0
                )
            {
                TryFixAndroidAdb();
            }
        }
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
                        Thread.Sleep(2000);//稍等一会儿
                        IPAddress ip = new DeviceSoftwareInfoGetter(_tooKit.Args.Serial).GetLocationIP();//获取设备IP

                        //连接到该设备
                        var connecter = new NetDeviceConnecter();
                        connecter.Init(new NetDeviceConnecterArgs() { IPEndPoint = new IPEndPoint(ip, (int)port) });
                        connecter.Run();
                    }

                    Logger.T("Fix android o adb successful....");
                });
            }
            catch (Exception e)
            {
                FixFailed(this, e);
                Logger.T("Fix android o adb failed....");
            }
        }
    }
}
