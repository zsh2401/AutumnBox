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
using AutumnBox.Support.CstmDebug;
using AutumnBox.Support.Helper;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Basic.FlowFramework
{
    public abstract partial class FunctionFlow<TArgs, TResult>
        : FunctionFlowBase, IOutSender, IForceStoppable, IDisposable
        where TArgs : FlowArgs, new()
        where TResult : FlowResult, new()
    {
        public event StartupEventHandler Startup;
        public event FinishedEventHandler<TResult> Finished;
        public event OutputReceivedEventHandler OutputReceived;
        public event ProcessStartedEventHandler ProcessStarted;
        public FlowStatus Status { get; private set; } = FlowStatus.Creating;
        public FunctionFlow()
        {
            _executer = new CExecuter();
            _resultTmp = new TResult();
            TAG = new LogSender(this.GetType().Name, true);
            Status = FlowStatus.Ready;
            _executer.OutputReceived += (s, e) =>
            {
                OnOutputReceived(e);
            };
        }
        public void Init(TArgs args)
        {
            Initialization(args);
        }
        public async void RunAsync()
        {
            await Task.Run(() =>
            {
                MainFlow();
            });
        }
        public TResult Run()
        {
            MainFlow();
            return _resultTmp;
        }
        public void ForceStop()
        {
            if (_pid == null) return;
            SystemHelper.KillProcessAndChildrens((int)_pid);
            isForceStoped = true;
        }
        public Stoper GetStoper()
        {
            return new Stoper(this);
        }
        public void Dispose() => Dispose(true);
    }
}
