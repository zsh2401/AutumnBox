/* =============================================================================*\
*
* Filename: VirtualButtonHider
* Description: 
*
* Version: 1.0
* Created: 2017/11/30 22:22:52 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;

namespace AutumnBox.Basic.Flows
{
    public class VirtualButtonHiderArgs : FlowArgs
    {
        public bool IsHide { get; set; } = true;
    }
    public class VirtualButtonHider : FunctionFlow<VirtualButtonHiderArgs, AdvanceResult>
    {
        private static readonly string _commandOfToHide = "settings put global policy_control immersive.navigation=*";
        private static readonly string _commandOfUnhide = "settings put global policy_control null";
        private ShellOutput Result;
        protected override OutputData MainMethod(ToolKit<VirtualButtonHiderArgs> toolKit)
        {
            if (toolKit.Args.IsHide)
            {
                Result = toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo.Serial, _commandOfToHide).ToShellOutput();
            }
            else
            {
                Result = toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo.Serial, _commandOfUnhide).ToShellOutput();
            }
            return Result.OutputData;
        }
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = Result.ReturnCode;
            result.ResultType = ResultType.MaybeSuccessful;
            if (result.ExitCode != 0)
            {
                result.ResultType = ResultType.Unsuccessful;
            }
        }
    }
}
