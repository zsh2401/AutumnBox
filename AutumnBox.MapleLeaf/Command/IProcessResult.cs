using AutumnBox.MapleLeaf.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Command
{
    public interface IProcessResult
    {
        int ExitCode { get; }
        IOutput Output { get; }
    }
}
