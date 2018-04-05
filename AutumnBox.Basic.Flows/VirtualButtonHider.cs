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
    /// <summary>
    /// 虚拟键隐藏器参数
    /// </summary>
    public class VirtualButtonHiderArgs : FlowArgs
    {
        /// <summary>
        /// 隐藏还是显示?默认为True
        /// </summary>
        public bool IsHide { get; set; } = true;
    }
    /// <summary>
    /// 虚拟隐藏器
    /// </summary>
    public class VirtualButtonHider : FunctionFlow<VirtualButtonHiderArgs, AdvanceResult>
    {
        private static readonly string _commandOfToHide = "settings put global policy_control immersive.navigation=*";
        private static readonly string _commandOfUnhide = "settings put global policy_control null";
        private AdvanceOutput Result;
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<VirtualButtonHiderArgs> toolKit)
        {
            if (toolKit.Args.IsHide)
            {
                Result = toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo.Serial, _commandOfToHide);
            }
            else
            {
                Result = toolKit.Executer.QuicklyShell(toolKit.Args.DevBasicInfo.Serial, _commandOfUnhide);
            }
            return Result;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = Result.GetExitCode();
            result.ResultType = ResultType.MaybeSuccessful;
            if (result.ExitCode != 0)
            {
                result.ResultType = ResultType.Unsuccessful;
            }
        }
    }
}
