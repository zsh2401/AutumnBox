/* =============================================================================*\
*
* Filename: NetDebuggingOpener
* Description: 
*
* Version: 1.0
* Created: 2017/11/30 22:18:15 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Flows.Result;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 网络调试开启器参数
    /// </summary>
    public class NetDebuggingOpenerArgs : FlowArgs
    {
        /// <summary>
        /// 开启的端口
        /// </summary>
        public uint Port { get; set; } = 5555;
    }
    /// <summary>
    /// 网络调试开启器
    /// </summary>
    public class NetDebuggingOpener : FunctionFlow<NetDebuggingOpenerArgs, AdvanceResult>
    {
        private AdvanceOutput _executeResult;
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<NetDebuggingOpenerArgs> toolKit)
        {
            _executeResult = toolKit.Ae($"tcpip {toolKit.Args.Port}");
            return _executeResult;
        }
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <param name="result"></param>
        protected override void AnalyzeResult(AdvanceResult result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = _executeResult.GetExitCode();
            result.ResultType = _executeResult.IsSuccessful ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}
