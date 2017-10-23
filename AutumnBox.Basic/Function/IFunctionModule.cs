using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Function
{
    public interface IFunctionModule : IDisposable
    {
        event DataReceivedEventHandler OutReceived;
        event DataReceivedEventHandler ErrorReceived;
        event EventHandler Startup;
        event FinishedEventHandler Finished;
        event ProcessStartedEventHandler CoreProcessStarted;
        ModuleStatus Status { get; }
        bool IsFinishedEventRegistered { get; }
        int CoreProcessPid { get; }
        bool WasFrociblyStop { get; }
        ModuleArgs Args { get; set; }
        void AsyncRun();
        ExecuteResult SyncRun();
        void KillProcess();
    }
}
