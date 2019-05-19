using AutumnBox.Basic.ManagedAdb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    class AdbManager : IAdbManager
    {
        public IAdbServer Server { get; } = LocalAdbServer.Instance;

        public FileInfo AdbFile { get; } = new FileInfo("Resources/AdbExecutable/adb.exe");

        public FileInfo FastbootFile { get; } = new FileInfo("Resources/AdbExecutable/fastboot.exe");

        public DirectoryInfo ToolsDir { get; } = new DirectoryInfo("Resources/AdbExecutable/");
    }
}
