/* =============================================================================*\
*
* Filename: MiFlash.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 19:54:11(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
/*我想要传达给你的话语*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Event;

namespace AutumnBox.Basic.Function.Modules
{
    /// <summary>
    /// 模拟的Miflash线刷功能模块,未完成,请勿使用
    /// </summary>
    public sealed class MiFlash : FunctionModule
    {
        private OutputData temtOut = new OutputData();
        private ABProcess MainProcess = new ABProcess();
        public MiFlasherArgs _Args;
        public MiFlash()
        {
            MainProcess.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    temtOut.OutAdd(e.Data);
                    LogT("Out : " + e.Data);
                    OnOutReceived(e);
                }

            };
            MainProcess.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    temtOut.ErrorAdd(e.Data);
                    LogT("Error : " + e.Data);
                    OnErrorReceived(e);
                }
            };
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this._Args = (e.ModuleArgs as MiFlasherArgs);
        }
        protected override OutputData MainMethod()
        {
            MainProcess.StartInfo.WorkingDirectory = @"adb\";
            temtOut.Append(MainProcess.RunToExited(_Args.batFileName, $"-s {DeviceID}"));
            return temtOut;
        }
        protected override void HandingOutput(ref ExecuteResult executeResult)
        {
            if (MainProcess.ExitCode == 1) executeResult.Level = ResultLevel.Unsuccessful;
            executeResult.Message = executeResult.OutputData.LineAll[executeResult.OutputData.LineAll.Count - 1];
        }
    }
}
