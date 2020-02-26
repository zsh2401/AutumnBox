using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.SERT.Shared
{
    public interface IRawScriptsProvider
    {
        Dictionary<int, string> Scripts { get; }
    }
}
