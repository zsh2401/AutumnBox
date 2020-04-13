using AutumnBox.GUI.Services.Impl.Arcylic;
using AutumnBox.GUI.Services.Impl.OS;
using AutumnBox.Leafx.Container.Support;
using System;
using System.Security.Principal;
using System.Windows;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IOperatingSystemService))]
    class OperatingSystem : IOperatingSystemService
    {
        public bool HaveAdministratorPermission
        {
            get
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public bool IsWindows10 => SystemInfo.IsWin10();

        public void CreateShortcut(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null)
        {
            ShortcutHelper.CreateShortcut(directory, shortcutName, targetPath, description, iconLocation);
        }

        public void CreateShortcutOnDesktop(string shortcutName, string targetPath, string description = null, string iconLocation = null)
        {
            ShortcutHelper.CreateShortcutOnDesktop(shortcutName, targetPath, description, iconLocation);
        }

        public void EnableHelpButton(Window window, Action onClickHandler)
        {
            HelpButtonHelper.EnableHelpButton(window, onClickHandler);
        }

        public void KillProcess(string processName)
        {
            TaskKill.Kill(processName);
        }

        public void RestartApplication()
        {
            throw new NotImplementedException();
        }

        public bool ThereIsOtherAutumnBoxProcess()
        {
            return OtherProcessChecker.ThereIsOtherAutumnBoxProcess() != null;
        }
    }
}
