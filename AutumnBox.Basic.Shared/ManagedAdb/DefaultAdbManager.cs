using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb
{
    class DefaultAdbManager : IAdbManager
    {
        public IAdbServer Server { get; set; }

        public FileInfo AdbFile { get; set; }

        public FileInfo FastbootFile { get; set; }

        public DirectoryInfo ToolsDir { get; set; }
    }
}
