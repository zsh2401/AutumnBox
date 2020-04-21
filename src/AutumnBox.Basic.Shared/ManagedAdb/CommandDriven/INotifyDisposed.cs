using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    public interface INotifyDisposed
    {
        event EventHandler Disposed;
    }
}
