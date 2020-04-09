using AutumnBox.Basic.ManagedAdb;
using System;
using System.IO;

namespace AutumnBox.ADBProvider
{
    internal class Win32AdbManager : IAdbManager
    {
        internal static readonly string ADB_TOOLS_PATH;

        static Win32AdbManager()
        {
            string temp = System.Environment.GetEnvironmentVariable("TEMP");
            ADB_TOOLS_PATH = Path.Combine(temp, "autumnbox_adb_tools/");
        }

        public IAdbServer Server { get; } = LocalAdbServer.Instance;

        public FileInfo AdbFile { get; } = new FileInfo(ADB_TOOLS_PATH + "adb.exe");

        public FileInfo FastbootFile { get; } = new FileInfo(ADB_TOOLS_PATH + "fastboot.exe");

        public DirectoryInfo ToolsDir { get; } = new DirectoryInfo(ADB_TOOLS_PATH);

    }
}
