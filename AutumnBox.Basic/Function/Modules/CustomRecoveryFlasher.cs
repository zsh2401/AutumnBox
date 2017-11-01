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
using AutumnBox.Support.CstmDebug;

namespace AutumnBox.Basic.Function.Modules
{
    /// <summary>
    /// recovery刷入器
    /// </summary>
    public sealed class CustomRecoveryFlasher : FunctionModule
    {
        private FileArgs _Args;
        protected override void AnalyzeArgs(ModuleArgs args)
        {
            base.AnalyzeArgs(args);
            _Args = (FileArgs)args;
        }
        protected override OutputData MainMethod()
        {
            Logger.D( "Start MainMethod");
            OutputData output = Fe($"flash recovery  \"{_Args.files[0]}\"");
            Fe($"boot \"{_Args.files[0]}\"");
            Logger.D(output.ToString());
            return output;
        }
    }
}
