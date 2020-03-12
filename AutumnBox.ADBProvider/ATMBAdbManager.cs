using AutumnBox.Basic.ManagedAdb;
using System.IO;

namespace AutumnBox.ADBProvider
{
    public class ATMBAdbManager : IAdbManager
    {
        private const string ADB_TOOLS_PATH = "adb_tools/";

        public IAdbServer Server { get; } = LocalAdbServer.Instance;

        public FileInfo AdbFile { get; } = new FileInfo(ADB_TOOLS_PATH + "adb.exe");

        public FileInfo FastbootFile { get; } = new FileInfo(ADB_TOOLS_PATH + "fastboot.exe");

        public DirectoryInfo ToolsDir { get; } = new DirectoryInfo(ADB_TOOLS_PATH);


        private static readonly string[] files = { "adb.exe", "AdbWinApi.dll", "AdbWinUsbApi.dll", "fastboot.exe", "libwinpthread-1.dll" };
        private const string FILES_NAMESPACE = "AutumnBox.ADBProvider.adb_tools";

        public void Extract()
        {
            if (!ToolsDir.Exists) ToolsDir.Create();
            var crtAssembly = this.GetType().Assembly;
            foreach (var file in files)
            {
                var targetFilePath = Path.Combine(ADB_TOOLS_PATH, file);
                if (!File.Exists(targetFilePath))
                {
                    using (var stream = crtAssembly.GetManifestResourceStream($"{FILES_NAMESPACE}.{file}"))
                    {
                        using (var fw = new FileStream(targetFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            stream.CopyTo(fw);
                            stream.Flush();
                        }
                    }
                }
            }
        }
    }
}
