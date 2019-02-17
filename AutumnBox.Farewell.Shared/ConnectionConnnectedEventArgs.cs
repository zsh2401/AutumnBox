using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Farewell
{
    public class ConnectionConnnectedEventArgs : EventArgs
    {
        public IConnection Connection { get; }
        public ConnectionConnnectedEventArgs(IConnection connection)
        {
            this.Connection = connection;
        }
    }
}
