/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/9 17:09:45
** filename: FilePusher.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class FilePusherArgs : FlowArgs
    {
        public string SourceFile { get; set; }
        public string SavePath { get; set; } = "/sdcard/fuck.tmp";
    }
    public sealed class FilePusher : FunctionFlow<FilePusherArgs>
    {
        private CommandExecuterResult exeResult;
        protected override OutputData MainMethod(ToolKit<FilePusherArgs> toolKit)
        {
            var command = Command.MakeForAdb($"push \"{toolKit.Args.SourceFile}\" \"{toolKit.Args.SavePath}\"");
            exeResult = toolKit.Executer.Execute(command);
            return exeResult.Output;
        }
        protected override void AnalyzeResult(FlowResult result)
        {
            base.AnalyzeResult(result);
            result.ResultType = exeResult.IsSuccessful ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}
