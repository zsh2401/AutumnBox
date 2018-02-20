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
    using AutumnBox.Basic.Function.Bundles;
    using AutumnBox.Basic.Function.Event;
    using System;

    public sealed class ScreenShoter : FunctionModule
    {
        ScreenShoterArgs _Args;
        protected override void Create(BundleForCreate bundle)
        {
            base.Create(bundle);
            _Args = (ScreenShoterArgs)bundle.Args;
        }
        protected override Output MainMethod(BundleForTools bundle)
        {
            var o = new Output();
            string fileName = $"{DateTime.Now.ToString("yyyy_MM_dd_hh_MM_ss")}.png";
            o.Append(bundle.Ae($"shell /system/bin/screencap -p /sdcard/{fileName}"));
            o.Append(bundle.Ae($"pull /sdcard/{fileName} " + _Args.LocalPath + "\\" + fileName));
            o.Append(bundle.Ae($"shell rm -rf /sdcard/{fileName}"));
            return o;
        }
    }
}
