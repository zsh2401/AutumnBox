/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/30 17:43:26 (UTC +8:00)
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
    public static class Broadcast
    {
        public static CommandExecuterResult Send(DeviceSerial device, string broadcast) {
           return ActivityManagerShared.Executer.QuicklyShell(device, $"am broadcast -a {broadcast}");
        }
        public static CommandExecuterResult SendWithData(DeviceSerial device, string data) {
            return Send(device, data);
        }
    }
}
