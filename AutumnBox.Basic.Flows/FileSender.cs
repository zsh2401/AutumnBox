/* =============================================================================*\
*
* Filename: FileSender
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 18:12:22 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Flows.Args;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.States;
using AutumnBox.Basic.Flows.Result;

namespace AutumnBox.Basic.Flows
{
    public class FileSender : FunctionFlow<FileSenderArgs, FileSenderResult>
    {
        protected override OutputData MainMethod(ToolKit<FileSenderArgs> toolKit)
        {
            string command = $"push \"{toolKit.Args.PathFrom}\" \"{toolKit.Args.PathTo + "/" + toolKit.Args.FileName}\"";
            return toolKit.Ae(command);
        }
        protected override void AnalyzeResult(FileSenderResult result)
        {
            base.AnalyzeResult(result);
            result.FileSendErrorType = FileSendErrorType.Unknow;
        }
    }
}
