using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    public interface ICommandDriver : IDisposable
    {
        ICommandProcedure CreateCommandProcedure(params string[] args);
    }
}
