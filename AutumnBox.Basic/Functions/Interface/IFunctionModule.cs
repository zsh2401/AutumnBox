using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions.Interface
{
    public interface IFunctionModule
    {
        event DataReceivedEventHandler OutReceived;
        event DataReceivedEventHandler ErrorReceived;
        event ProcessStartedEventHandler ProcessStarted;
        event StartEventHandler Started;
        event FinishEventHandler Finished;
        void Run();
    }
}
