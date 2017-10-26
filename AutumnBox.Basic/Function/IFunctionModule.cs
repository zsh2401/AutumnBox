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
    public interface IFunctionModule
    {
        event DataReceivedEventHandler OutReceived;
        event DataReceivedEventHandler ErrorReceived;
        event StartupEventHandler Startup;
        event FinishedEventHandler Finished;
        event ProcessStartedEventHandler CoreProcessStarted;
        ModuleStatus Status { get; }
        bool IsFinishedEventRegistered { get; }
        ExecuteResult Run(ModuleArgs args);
        void BeginRun(ModuleArgs args);
        void ForceStop();
    }
}
