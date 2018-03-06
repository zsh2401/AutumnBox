/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/9 17:09:45
** filename: FilePusher.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class FilePusherArgs : FlowArgs
    {
        public FileInfo SourceFileInfo
        {
            get
            {
                return new FileInfo(SourceFile);
            }
        }
        public string SourceFile { private get; set; }
        public string SavePath { get; set; } = "/sdcard/";
    }
    [LogProperty(TAG ="File pushing")]
    public sealed class FilePusher : FunctionFlow<FilePusherArgs,AdvanceResult>
    {
        private AdvanceOutput exeResult;
        protected override Output MainMethod(ToolKit<FilePusherArgs> toolKit)
        {
            var command = Command.MakeForAdb(
                $"push \"{toolKit.Args.SourceFileInfo.FullName}\" \"{toolKit.Args.SavePath + toolKit.Args.SourceFileInfo.Name}\""
                );
            exeResult = toolKit.Executer.Execute(command);
            return exeResult;
        }
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = exeResult.ExitCode;
            result.ResultType = exeResult.IsSuccessful ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}
