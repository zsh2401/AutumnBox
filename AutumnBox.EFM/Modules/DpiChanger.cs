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
using AutumnBox.Basic.Function.Event;

namespace AutumnBox.Basic.Function.Modules
{
    public class DpiChanger : FunctionModule
    {
        private int dpi;
        protected override void AnalyzeArgs(ModuleArgs args)
        {
            base.AnalyzeArgs(args);
            dpi = ((DpiChangerArgs)args).Dpi;
        }
        protected override OutputData MainMethod()
        {
            OutputData o = new OutputData();
            o.OutSender = Executer;
            Ae("shell wm density 400");
            Ae("adb reboot");
            return o;
        }
    }
}
