/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:31:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling.Adb
{
    public class AdbCommand : ProcessBasedCommand
    {
        public AdbCommand(string args) : base(AdbProtocol.ADB_EXECUTABLE_FILE_PATH, args)
        {
        }
        public AdbCommand(IDevice device, string args) :
            base(AdbProtocol.ADB_EXECUTABLE_FILE_PATH, $"-s {device.SerialNumber} {args}")
        {

        }
    }
}
