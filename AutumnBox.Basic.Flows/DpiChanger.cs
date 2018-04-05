/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 17:29:56 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows.Result;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// DPI修改器参数
    /// </summary>
    public class DpiChangerArgs :FlowArgs{
        /// <summary>
        /// 目标DPI
        /// </summary>
        public int Dpi { get; set; }
    }
    /// <summary>
    /// DPI修改器
    /// </summary>
    public class DpiChanger : FunctionFlow<DpiChangerArgs,AdvanceResult>
    {
        int exitCode = 1;
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<DpiChangerArgs> toolKit)
        {
            var outputBuilder = new OutputBuilder();
            outputBuilder.Register(toolKit.Executer);
            exitCode =  toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo.Serial, $"wm density {toolKit.Args.Dpi}").GetExitCode();
            if (exitCode == 0) {
                toolKit.Ae("reboot");
            }
            return outputBuilder.Result;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = exitCode;
            result.ResultType = (exitCode == 0)? ResultType.Successful:ResultType.MaybeUnsuccessful;
        }
    }
}
