using AutumnBox.Basic;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using AutumnBox.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace AutumnBox.ADBProvider
{
    public class Win32AdbManager : StandardAdbManager, IAdbManager
    {
        protected override DirectoryInfo InitializeClientFiles()
        {
            string temp = Environment.GetEnvironmentVariable("TEMP");
            var toolsPath = Path.Combine(temp, "autumnbox_adb_tools/");
            var toolsDir = new DirectoryInfo(toolsPath);
            if (!toolsDir.Exists) toolsDir.Create();
            ExtractFilesTo(toolsDir);
            return toolsDir;
        }

        protected override IPEndPoint StartServer()
        {
            var random = new Random();
            ushort port;
            do
            {
                port = (ushort)random.Next(IPEndPoint.MinPort, IPEndPoint.MaxPort);
            } while (PortIsUsinngNow(port));
            using (var cmd =
                new CommandProcedure("adb.exe", $"-P{port} start-server"))
            {
                cmd.KillChildWhenDisposing = false;
                cmd.OutputReceived += (s, e) =>
                {
                    SLogger<Win32AdbManager>.Info($"adb server starting: {e.Text}");
                };
                cmd.Disposed += (s, e) => SLogger<Win32AdbManager>.Info("Command start-adb disposed");
                cmd.Execute();
            }
            return new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
        }

        private bool PortIsUsinngNow(ushort port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }

        private static readonly string[] files = { "adb.exe", "AdbWinApi.dll", "AdbWinUsbApi.dll", "fastboot.exe", "libwinpthread-1.dll" };
        private const string FILES_NAMESPACE = "AutumnBox.ADBProvider.adb_tools";

        public void ExtractFilesTo(DirectoryInfo dir)
        {
            var crtAssembly = this.GetType().Assembly;
            foreach (var file in files)
            {
                var targetFilePath = Path.Combine(dir.FullName, file);
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
