using AutumnBox.MapleLeaf.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Basis
{
    public interface IProcessResult
    {
        int ExitCode { get; }
        IOutput Output { get; }
    }
}
