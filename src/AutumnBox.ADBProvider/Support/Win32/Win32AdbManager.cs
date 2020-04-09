using AutumnBox.Basic.ManagedAdb;
using System.IO;

namespace AutumnBox.ADBProvider
{
    internal class Win32AdbManager : IAdbManager
    {
        internal const string ADB_TOOLS_PATH = "adb_tools/";

        public IAdbServer Server { get; } = LocalAdbServer.Instance;

        public FileInfo AdbFile { get; } = new FileInfo(ADB_TOOLS_PATH + "adb.exe");

        public FileInfo FastbootFile { get; } = new FileInfo(ADB_TOOLS_PATH + "fastboot.exe");

        public DirectoryInfo ToolsDir { get; } = new DirectoryInfo(ADB_TOOLS_PATH);

    }
}
