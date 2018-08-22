using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Data
{
    public interface IOutput
    {
        string All { get; }
        string Std { get; }
        string Error { get; }
    }
}
