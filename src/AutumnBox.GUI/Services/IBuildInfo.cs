using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    internal interface IBuildInfo
    {
        string LatestCommit { get; }
        string DateTime { get; }
        string LatestTag { get; }
        string Version { get; }
    }
}
