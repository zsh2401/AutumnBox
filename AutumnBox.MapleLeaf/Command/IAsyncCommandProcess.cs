using AutumnBox.MapleLeaf.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Basis
{
    public interface IAsyncCommandProcess : IDisposable
    {
        event OutputReceivedEventHandler OutputReceived;
        event AsyncCommandProcessFinishedEventHandler Finished;
        bool IsExecuting { get; }
        void BeginExecute();
        void Stop();
    }
}
