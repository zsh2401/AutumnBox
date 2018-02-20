/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/30 17:57:28 (UTC +8:00)
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
    public static class Activity
    {
        public static AdvanceOutput Start(DeviceSerial device, string pkgName, string className,bool createNewOnExist=false) {
            string arg = createNewOnExist ?"-n":"";
            return ActivityManagerShared.Executer.QuicklyShell(device,$"am start {arg} {pkgName}/.{className}");
        }
    }
}
