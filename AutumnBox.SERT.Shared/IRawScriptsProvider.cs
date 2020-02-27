using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.SERT
{
    public interface IRawScriptsProvider
    {
        Dictionary<int, string> Scripts { get; }
    }
}
