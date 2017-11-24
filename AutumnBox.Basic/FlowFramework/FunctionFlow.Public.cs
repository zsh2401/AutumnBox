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
using AutumnBox.Support.CstmDebug;
using AutumnBox.Support.Helper;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Basic.FlowFramework
{
    public abstract partial class FunctionFlow<ARGS_T, RESUL_T>
        : IOutSender, IForceStoppable, IDisposable
        where ARGS_T : FlowArgs
        where RESUL_T : FlowResult, new()
    {
        public event StartupEventHandler Startup;
        public event FinishedEventHandler<RESUL_T> Finished;
        public event OutputReceivedEventHandler OutputReceived;
        public event ProcessStartedEventHandler ProcessStarted;
        public FlowStatus Status { get; private set; }
        public FunctionFlow()
        {
            Status = FlowStatus.Creating;
            TAG = new LogSender(this.GetType().Name, true);
            Status = FlowStatus.Ready;
            _executer.OutputReceived += (s, e) =>
            {
                OnOutputReceived(e);
            };
        }
        public void Init(ARGS_T args)
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
        public RESUL_T Run() { MainFlow(); throw new NotImplementedException(); }
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
        protected void Dispose(bool disposing)
        {
            GC.SuppressFinalize(this);
        }
    }
}
