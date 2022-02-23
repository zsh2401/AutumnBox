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

        public FileInfo AdbFile { get; } = new FileInfo(System.Windows.Forms.Application.StartupPath+"/Resources/AdbExecutable/adb.exe");

        public FileInfo FastbootFile { get; } = new FileInfo(System.Windows.Forms.Application.StartupPath+"/Resources/AdbExecutable/fastboot.exe");

        public DirectoryInfo ToolsDir { get; } = new DirectoryInfo(System.Windows.Forms.Application.StartupPath+"/Resources/AdbExecutable/");
    }
}
