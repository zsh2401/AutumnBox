/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 16:17:24 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

namespace AutumnBox.GUI.Util
{
    static class Self
    {
        /// <summary>
        /// 获取当前AutumnBox版本
        /// </summary>
        public static Version Version
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        /// <summary>
        /// 判断是否拥有管理员权限
        /// </summary>
        public static bool HaveAdminPermission
        {
            get
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        /// <summary>
        /// 判断是否有其它秋之盒进程
        /// </summary>
        public static bool HaveOtherAutumnBoxProcess
        {
            get
            {
                return Process.GetProcessesByName("AutumnBox.GUI").Length > 1;
            }
        }
        /// <summary>
        /// 重启程序
        /// </summary>
        /// <param name="asAdmin"></param>
        public static void Restart(bool asAdmin)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Path.Combine("..", "AutumnBox-秋之盒.exe"));
            var args = new List<string>
            {
                $"-waitfor {Process.GetCurrentProcess().Id}"
            };
            if (asAdmin)
            {
                args.Add("-tryadmin");
            }
            startInfo.Arguments = string.Join(" ", args);
            SLogger.Debug("Self", startInfo.FileName + "  " + startInfo.Arguments);
            Process.Start(startInfo);
            App.Current.Dispatcher.Invoke(() =>
            {
                App.Current.Shutdown(0);
            });
        }
    }
}
