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
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.FlowFramework.Events;
using AutumnBox.Basic.FlowFramework.States;
using AutumnBox.Support.CstmDebug;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Basic.FlowFramework
{
    partial class FunctionFlow<TArgs, TResult>
    {
        protected readonly LogSender TAG;
        protected TArgs Args { get; private set; }
        protected virtual void Initialization(TArgs moduleArgs)
        {
            Args = moduleArgs;
        }
        protected virtual CheckResult Check() { return CheckResult.OK; }
        protected virtual void OnStartup(StartupEventArgs e)
        {
            Task.Run(() =>
            {
                Startup?.Invoke(this, e);
            });
        }
        protected abstract OutputData MainMethod(ToolKit<TArgs> toolKit);
        protected virtual void OnOutputReceived(OutputReceivedEventArgs e)
        {
            Task.Run(() =>
            {
                OutputReceived?.Invoke(this, e);
            });
        }
        protected virtual void OnProcessStarted(ProcessStartedEventArgs e)
        {
            _pid = e.Pid;
            Task.Run(() =>
            {
                ProcessStarted?.Invoke(this, e);
            });
        }
        protected virtual void AnalyzeResult(TResult result)
        {
            result.ResultType = isForceStoped ? ResultType.Unsuccessful : result.ResultType;
        }
        protected virtual void OnFinished(FinishedEventArgs<TResult> e)
        {
            Task.Run(() =>
            {
                _resultTmp = e.Result;
                if (Finished != null)
                {
                    Finished(this, e);
                }
                else
                {
                    OnAnyFinished(this, new FinishedEventArgs<FlowResult>(e.Result));
                }
            });
        }
        protected void Dispose(bool disposing)
        {
            if (disposing) _executer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
