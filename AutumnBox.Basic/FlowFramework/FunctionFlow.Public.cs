/* =============================================================================*\
*
* Filename: FunctionFlow.Public
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
using AutumnBox.Support.CstmDebug;
using AutumnBox.Support.Helper;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Basic.FlowFramework
{
    public abstract partial class FunctionFlow<TArgs, TResult>
        : FunctionFlowBase, IOutSender, IForceStoppable, IDisposable, ICompletable
        where TArgs : FlowArgs, new()
        where TResult : FlowResult, new()
    {
        public event StartupEventHandler Startup;
        public event FinishedEventHandler<TResult> Finished;
        public event OutputReceivedEventHandler OutputReceived;
        public event ProcessStartedEventHandler ProcessStarted;
        public event EventHandler NoArgFinished;
        public bool MustTiggerAnyFinishedEvent { get; set; } = false;
        public FunctionFlow()
        {
            _executer = new CommandExecuter();
            _resultTmp = new TResult();
            TAG = new LogSender(this,this.GetType().Name, true);
            _executer.ProcessStarted += (s, e) =>
            {
                OnProcessStarted(e);
            };
            _executer.OutputReceived += (s, e) =>
            {
                OnOutputReceived(e);
            };
        }
        public void Init(TArgs args)
        {
            Initialization(args);
        }
        private bool _isSync = false;
        public async void RunAsync()
        {
            Logger.T("Run start async....");
            if (_args == null) throw new Exception("have not init!!!! try Init()");
            await Task.Run(() =>
            {
                MainFlow();
            });
        }
        public async void RunAsync(TArgs args)
        {
            Initialization(args);
            await Task.Run(() =>
            {
                MainFlow();
            });
        }
        public TResult Run(TArgs args)
        {
            Initialization(args);
            return Run();
        }
        public TResult Run()
        {
            _isSync = true;
            Logger.T("Run start sync....");
            if (_args == null) throw new Exception("have not init!!!! try Init()");
            MainFlow();
            return _resultTmp;
        }
        public void ForceStop()
        {
            Logger.T("Try to force Stop");
            if (_pid == null) return;
            SystemHelper.KillProcessAndChildrens((int)_pid);
            isForceStoped = true;
            Logger.T("Force stoped...");
        }
        public Stoper GetStoper()
        {
            return new Stoper(this);
        }
        public void Dispose() {
            ForceStop();
        }
    }
}
