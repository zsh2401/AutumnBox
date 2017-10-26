/* =============================================================================*\
*
* Filename: CustomRecoveryFlasher.cs
* Description: 
*
* Version: 1.0
* Created: 9/5/2017 18:24:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Event;
using AutumnBox.Basic.Util;
using static AutumnBox.Basic.Debug;
namespace AutumnBox.Basic.Function.Modules
{
    /// <summary>
    /// recovery刷入器
    /// </summary>
    public sealed class CustomRecoveryFlasher : FunctionModule
    {
        private FileArgs _Args;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _Args = (FileArgs)e.ModuleArgs;
        }
        protected override OutputData MainMethod()
        {
            Logger.D(TAG, "Start MainMethod");
            OutputData output = Ae($"flash recovery  \"{_Args.files[0]}\"");
            Ae($"boot \"{_Args.files[0]}\"");
            return output;
        }
    }
}
