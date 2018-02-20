/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/30 17:54:00 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.ActivityManager
{
    public static class Service
    {
        public static AdvanceOutput RunService(DeviceSerial device,
            string pkgName, 
            string className) {
            return ActivityManagerShared.Executer.QuicklyShell(device,$"am startservice {pkgName}/.{className}");
        }
    }
}
