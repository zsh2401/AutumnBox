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
using AutumnBox.Basic.Function.Bundles;
using AutumnBox.Support.CstmDebug;

namespace AutumnBox.Basic.Function.Modules
{
    public class DpiChanger : FunctionModule
    {
        bool isSuccess;
        private int dpi;
        protected override void Create(BundleForCreate bundle)
        {
            base.Create(bundle);
            dpi = ((DpiChangerArgs)bundle.Args).Dpi;
        }
        protected override OutputData MainMethod(BundleForTools bundle)
        {
            OutputData o = new OutputData
            {
                OutSender = bundle.Executer
            };
            bundle.Executer.QuicklyShell(bundle.DeviceID, $"wm density {dpi}", out isSuccess);
            if (isSuccess)
            {
                bundle.Ae("reboot");
            }
            Logger.D("maybe finished....the output ->" + o.ToString());
            return o;
        }
        protected override void AnalyzeOutput(BundleForAnalyzingResult bundle)
        {
            base.AnalyzeOutput(bundle);
            if (!isSuccess) bundle.Result.Level = ResultLevel.Unsuccessful;
        }
    }
}
