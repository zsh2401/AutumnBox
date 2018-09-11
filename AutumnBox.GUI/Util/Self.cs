/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 16:17:24 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Diagnostics;
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
    }
}
