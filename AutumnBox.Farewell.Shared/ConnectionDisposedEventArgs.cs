using System;

namespace AutumnBox.Farewell
{
    public class ConnectionDisposedEventArgs : EventArgs
    {
        public IConnection Connection { get; }
        public ConnectionDisposedEventArgs(IConnection connection)
        {
            this.Connection = connection;
        }
    }
}
