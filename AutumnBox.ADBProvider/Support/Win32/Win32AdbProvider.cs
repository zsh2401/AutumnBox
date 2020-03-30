/*

* ==============================================================================
*
* Filename: Win32AdbProvider
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:06:36
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.ManagedAdb;
using System.IO;

namespace AutumnBox.ADBProvider.Support.Win32
{
    internal class Win32AdbProvider : IAdbProvider
    {

        private static readonly string[] files = { "adb.exe", "AdbWinApi.dll", "AdbWinUsbApi.dll", "fastboot.exe", "libwinpthread-1.dll" };
        private const string FILES_NAMESPACE = "AutumnBox.ADBProvider.adb_tools";

        public IAdbManager AdbManager => new Win32AdbManager();



        public void Load()
        {
            if (!AdbManager.ToolsDir.Exists) AdbManager.ToolsDir.Create();
            var crtAssembly = this.GetType().Assembly;
            foreach (var file in files)
            {
                var targetFilePath = Path.Combine(Win32AdbManager.ADB_TOOLS_PATH, file);
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
