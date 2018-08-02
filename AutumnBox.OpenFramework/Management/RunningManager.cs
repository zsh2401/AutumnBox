/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 19:42:42 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Management
{
    public static class RunningManager
    {
        private static List<IExtensionWarpper> runningWarppers = new List<IExtensionWarpper>();
        public static void AddRuningWarpper(IExtensionWarpper warpper)
        {
            runningWarppers.Add(warpper);
        }
        public static void RemoveRunningWarpper(IExtensionWarpper warpper)
        {
            runningWarppers.Remove(warpper);
        }
    }
}
