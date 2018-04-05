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
using System.IO;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 文件推送器参数
    /// </summary>
    public class FilePusherArgs : FlowArgs
    {
        /// <summary>
        /// 源文件信息
        /// </summary>
        public FileInfo SourceFileInfo
        {
            get
            {
                return new FileInfo(SourceFile);
            }
        }
        /// <summary>
        /// 源文件
        /// </summary>
        public string SourceFile { private get; set; }
        /// <summary>
        /// 推送到的设备路径
        /// </summary>
        public string SavePath { get; set; } = "/sdcard/";
    }
    /// <summary>
    /// 文件推送器
    /// </summary>
    public sealed class FilePusher : FunctionFlow<FilePusherArgs,AdvanceResult>
    {
        private AdvanceOutput exeResult;
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<FilePusherArgs> toolKit)
        {
            var command = Command.MakeForAdb(
                $"push \"{toolKit.Args.SourceFileInfo.FullName}\" \"{toolKit.Args.SavePath + toolKit.Args.SourceFileInfo.Name}\""
                );
            exeResult = toolKit.Executer.Execute(command);
            return exeResult;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = exeResult.GetExitCode();
            result.ResultType = exeResult.IsSuccessful ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}
