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
using AutumnBox.Basic.FlowFramework.States;
using AutumnBox.Basic.Function.Bundles;

namespace AutumnBox.Basic.FlowFramework
{
    public abstract class FunctionFlow
    {
        protected FlowArgs Args { get; private set; }
        public event StartupEventHandler Startup;
        public event FinishedEventHandler Finished;
        public event ProcessStartedEventHandler ProcessStarted;
        public event OutputReceivedEventHandler OutputReceived;
        protected internal virtual void Create(FlowArgs moduleArgs)
        {
            Args = moduleArgs;
        }
        protected internal virtual CheckResult Check() { return CheckResult.OK; }
        protected internal virtual void OnStartup(StartupEventArgs e) { }
        protected internal abstract OutputData MainMethod(ToolKit toolKit);
        protected virtual void OnProcessStarted(ProcessStartedEventArgs e) { }
        protected virtual void OnOutputReceived(OutputReceivedEventArgs e) { }
        protected internal virtual void AnalyzeResuslt(BundleForAnalyzing bundle) { }
        protected virtual void OnFinished(FinishedEventArgs e) { }
    }
}
