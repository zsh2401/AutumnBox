using AutumnBox.Basic.ManagedAdb.CommandDriven;
using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace AutumnBox.ADBProvider
{
    public class Win32AdbManager : StandardAdbManager, IAdbManager
    {
        private static readonly string[] files = { "adb.exe", "AdbWinApi.dll", "AdbWinUsbApi.dll", "fastboot.exe", "libwinpthread-1.dll" };
        private const string FILES_NAMESPACE = "AutumnBox.ADBProvider.adb_tools";

        protected override DirectoryInfo InitializeClientFiles()
        {
            string temp = Environment.GetEnvironmentVariable("TEMP");
            var toolsPath = Path.Combine(temp, "autumnbox_adb_tools/");
            var toolsDir = new DirectoryInfo(toolsPath);
            if (!toolsDir.Exists) toolsDir.Create();
            ExtractFilesTo(toolsDir);
            return toolsDir;
        }

        protected override IPEndPoint StartServer(ushort port = 6605)
        {
            //在随机端口启动ADB服务
            var random = new Random();
            ushort _randomPort;
            do
            {
                _randomPort = (ushort)random.Next(IPEndPoint.MinPort, IPEndPoint.MaxPort);
            } while (PortIsUsinngNow(_randomPort));
            return base.StartServer(_randomPort);
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
