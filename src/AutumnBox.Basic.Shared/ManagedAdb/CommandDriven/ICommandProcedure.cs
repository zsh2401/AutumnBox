using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    public interface ICommandProcedure : IDisposable
    {
        event OutputReceivedEventHandler OutputReceived;
        event OutputReceivedEventHandler ErrorReceived;
        CommandStatus Status { get; }
        ICommandResult Result { get; }
        Task<ICommandResult> ExecuteAsync();
        ICommandResult Execute();
    }
}
