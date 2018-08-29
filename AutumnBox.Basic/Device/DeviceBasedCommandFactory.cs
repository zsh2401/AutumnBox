/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 5:24:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.DPCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    public static class DeviceBasedCommandFactory
    {
        public static void Append(string commandId) { }
        public static void Remove(string commandId) { }
        public static ICommand GetCommand(string comamndId)
        {
            throw new NotImplementedException();
        }
    }
}
