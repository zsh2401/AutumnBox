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
    public class NetDebuggingOpenerArgs : FlowArgs
    {
        public uint Port { get; set; } = 5555;
    }
    public class NetDebuggingOpener : FunctionFlow<NetDebuggingOpenerArgs, FlowResultWithCode>
    {
        private CommandExecuterResult _executeResult;
        protected override OutputData MainMethod(ToolKit<NetDebuggingOpenerArgs> toolKit)
        {
            _executeResult = toolKit.Ae($"tcpip {toolKit.Args.Port}");
            return _executeResult.Output;
        }
        protected override void AnalyzeResult(FlowResultWithCode result)
        {
            base.AnalyzeResult(result);
            result.ExitCode = _executeResult.ExitCode;
            result.ResultType = _executeResult.IsSuccessful ? ResultType.Successful : ResultType.Unsuccessful;
        }
    }
}
