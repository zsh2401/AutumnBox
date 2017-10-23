/* =============================================================================*\
*
* Filename: ScreenShoter.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 03:06:56(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Function.Modules
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Function.Args;
    public sealed class ScreenShoter : FunctionModule
    {
        ScreenShoterArgs _Args;
        protected override void HandlingModuleArgs(ModuleArgs e)
        {
            base.HandlingModuleArgs(e);
            _Args = (ScreenShoterArgs)e;
        }
        protected override OutputData MainMethod()
        {
            OutputData o = new OutputData();
            o.Append(Ae("shell /system/bin/screencap -p /sdcard/screenshot.png"));
            o.Append(Ae("pull /sdcard/screenshot.png " + _Args.LocalPath));
            o.Append(Ae("shell rm -rf /sdcard/screenshot.png"));
            return o;
        }
    }
}
