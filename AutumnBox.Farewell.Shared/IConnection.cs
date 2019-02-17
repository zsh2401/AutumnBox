using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Farewell
{
    public interface IConnection : IDisposable
    {
        IDevice Owner { get; }
        void Connect();
        byte[] ExecuteCommand(string command);
        bool IsAlive { get; }
        event EventHandler<ConnectionConnnectedEventArgs> Connected;
        event EventHandler<ConnectionDisposedEventArgs> Disposed;
    }
}
