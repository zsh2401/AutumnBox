/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/9 13:03:09
** filename: FilePuller.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;

namespace AutumnBox.Basic.Flows
{
    public class FilePullerArgs : FlowArgs
    {
        public string FilePathOnDevice { get; set; }
        public string SavePath { get; set; } = ".";
    }
    public sealed class FilePuller : FunctionFlow<FilePullerArgs, AdvanceResult>
    {
        private CommandExecuterResult result;
        protected override OutputData MainMethod(ToolKit<FilePullerArgs> toolKit)
        {
            var command = Command.MakeForAdb($"pull \"{toolKit.Args.FilePathOnDevice}\" \"{toolKit.Args.SavePath}\"");
            result = toolKit.Executer.Execute(command);
            return result.Output;
        }
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = result.ExitCode;
            result.ResultType = result.IsSuccess ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}
