using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Logging.Management
{
    public interface ILog
    {
        DateTime Time { get; }
        string Level { get; }
        string Category { get; }
        string Message { get; }
    }
}
