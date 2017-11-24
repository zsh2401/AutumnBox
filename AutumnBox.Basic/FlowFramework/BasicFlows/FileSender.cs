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
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.FlowFramework.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.FlowFramework.BasicFlows
{
    public class FileSender : FunctionFlow<FileSenderArgs, FileSenderResult>
    {
        protected override OutputData MainMethod(ToolKit<FileSenderArgs> toolKit)
        {
            var o = new OutputData();
            string command = $"push \"{toolKit.Args.PathFrom}\" \"{toolKit.Args.PathTo + "/" + toolKit.Args.FileName}\"";
            o = toolKit.Ae(command);
            return o;
        }
        protected override void AnalyzeResuslt(FileSenderResult result)
        {
            base.AnalyzeResuslt(result);
            result.FileSendErrorType = FileSendErrorType.Unknow;
        }
    }
}
