/* =============================================================================*\
*
* Filename: DpiChanger.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 19:59:36(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Support.CstmDebug;

namespace AutumnBox.Basic.Function.Modules
{
    public class DpiChanger : FunctionModule
    {
        bool isSuccess;
        private int dpi;
        protected override void AnalyzeArgs(ModuleArgs args)
        {
            base.AnalyzeArgs(args);
            dpi = ((DpiChangerArgs)args).Dpi;
        }
        protected override OutputData MainMethod()
        {
            OutputData o = new OutputData
            {
                OutSender = Executer
            };
            Executer.QuicklyShell(DeviceID, $"wm density {dpi}", out isSuccess);
            if (isSuccess)
            {
                Ae("reboot");
            }
            Logger.D("maybe finished....the output ->" + o.ToString());
            return o;
        }
        protected override void AnalyzeOutput(ref ExecuteResult executeResult)
        {
            base.AnalyzeOutput(ref executeResult);
            if (!isSuccess) executeResult.Level = ResultLevel.Unsuccessful;
        }
    }
}
