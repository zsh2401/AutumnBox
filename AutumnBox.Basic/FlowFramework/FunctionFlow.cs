/* =============================================================================*\
*
* Filename: FunctionFlow
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 14:52:17 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework.Args;
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.FlowFramework.Events;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Bundles;

namespace AutumnBox.Basic.FlowFramework
{
    public abstract class FunctionFlow<ARGS_T,RESULT_T>
        where RESULT_T : Result where ARGS_T : FlowArgs
    {
        protected ARGS_T Args { get; private set; }
        public event StartupEventHandler Startup;
        public event FinishedEventHandler Finished;
        public event ProcessStartedEventHandler ProcessStarted;
        public event OutputReceivedEventHandler OutputReceived;
        public static FunctionFlow<ARGS_T,RESULT_T> Get(ARGS_T args) {
            throw new System.Exception();
        }
        protected internal virtual void Create(ARGS_T moduleArgs)
        {
            Args = moduleArgs;
        }
        protected internal virtual CheckResult Check() { return CheckResult.OK; }
        protected internal virtual void OnStartup(StartupEventArgs e) { }
        protected internal abstract void MainMethod(ToolKit<ARGS_T> toolKit);
        protected virtual void OnProcessStarted(ProcessStartedEventArgs e) { }
        protected virtual void OnOutputReceived(OutputReceivedEventArgs e) { }
        protected internal virtual void AnalyzeResuslt(BundleForAnalyzing<RESULT_T> bundle) { }
        protected virtual void OnFinished(FinishedEventArgs e) { }
    }
}
