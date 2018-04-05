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
    /// <summary>
    /// 文件拉取器参数
    /// </summary>
    public sealed class FilePullerArgs : FlowArgs
    {
        /// <summary>
        /// 要拉取的文件路径
        /// </summary>
        public string FilePathOnDevice { get; set; }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath { get; set; } = ".";
    }
    /// <summary>
    /// 文件拉取器
    /// </summary>
    public sealed class FilePuller : FunctionFlow<FilePullerArgs, AdvanceResult>
    {
        private AdvanceOutput result;
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<FilePullerArgs> toolKit)
        {
            var command = Command.MakeForAdb($"pull \"{toolKit.Args.FilePathOnDevice}\" \"{toolKit.Args.SavePath}\"");
            result = toolKit.Executer.Execute(command);
            return result;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = result.ExitCode;
            result.ResultType = result.IsSuccess ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}
