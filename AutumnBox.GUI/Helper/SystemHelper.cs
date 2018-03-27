/* =============================================================================*\
*
* Filename: SystemHelper.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 05:04:40(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Support.CstmDebug;
using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace AutumnBox.GUI.Helper
{
    /// <summary>
    /// SystemHelper static class, static methods about System
    /// </summary>
    internal static class SystemHelper
    {
        /// <summary>
        /// Create Shortcut
        /// </summary>
        /// <param name="directory">快捷方式所处的文件夹</param>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"，
        /// 例如System.Environment.SystemDirectory + "\\" + "shell32.dll, 165"</param>
        /// <remarks></remarks>
        public static void CreateShortcut(
            string directory, string shortcutName,
            string targetPath,
            string description = null, string iconLocation = null)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
            shortcut.TargetPath = targetPath;//指定目标路径
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
            shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
            shortcut.Description = description;//设置备注
            shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;//设置图标路径
            shortcut.Save();//保存快捷方式
        }
        /// <summary>
        /// 创建桌面快捷方式
        /// </summary>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"</param>
        /// <remarks></remarks>
        public static void CreateShortcutOnDesktop(string shortcutName, string targetPath,
            string description = null, string iconLocation = null)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);//获取桌面文件夹路径
            CreateShortcut(desktop, shortcutName, targetPath, description, iconLocation);
        }
        public static DateTime CompiledDate
        {
            get
            {
                var o = (CompiledDateAttribute)Attribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(), typeof(CompiledDateAttribute));
                return o.DateTime;
            }
        }
        public static Version CurrentVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
        public static void KillProcess(string processName)
        {
            var list = Process.GetProcessesByName(processName);
            foreach (Process p in list)
            {
                p.Kill();
            }
        }
        public static bool IsWin10
        {
            get
            {
                return Environment.OSVersion.Version.Major == 10;
            }
        }
        public static bool HaveAdminPermission
        {
            get
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        public static void ChangeAdbProt(uint port = 24010)
        {
            Environment.SetEnvironmentVariable("ADB_PORT", port.ToString(), EnvironmentVariableTarget.User);
        }
        public static bool HaveOtherAutumnBoxProcess
        {
            get
            {
                return Process.GetProcessesByName("AutumnBox.GUI").Length > 1;
            }
        }
    }
}
