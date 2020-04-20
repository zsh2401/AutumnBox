using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    public interface ICommandResult
    {
        int? ExitCode { get; }
        Output Output { get; }
    }
}
