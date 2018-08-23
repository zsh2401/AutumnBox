using AutumnBox.MapleLeaf.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace AutumnBox.MapleLeaf.Command
{
    public interface IAsyncCommandProcess : ICommand, IDisposable
    {
        event OutputReceivedEventHandler OutputReceived;
        bool IsExecuting { get; }
        Task<IProcessResult> ExecuteAsync(CancellationToken cancellationToken);
    }
}
