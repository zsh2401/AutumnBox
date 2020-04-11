using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    interface IOperatingSystemService
    {
        void KillProcess(string processName);
        void CreateShortcut();
        bool ThereIsOtherAutumnBoxProcess();
        bool HaveAdministratorPermission { get; }
        bool IsWindows10 { get; }
        void RestartApplication();
        void EnableHelpButton(IntPtr hWnd, Action onClickHandler);
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
        void CreateShortcut(
           string directory, string shortcutName,
           string targetPath,
           string description = null, string iconLocation = null);
        /// <summary>
        /// 创建桌面快捷方式
        /// </summary>
        /// <param name="shortcutName">快捷方式名称</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标路径，格式为"可执行文件或DLL路径, 图标编号"</param>
        /// <remarks></remarks>
        void CreateShortcutOnDesktop(string shortcutName, string targetPath,
          string description = null, string iconLocation = null);
    }
}
