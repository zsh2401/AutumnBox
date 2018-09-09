/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 15:58:55 (UTC +8:00)
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
    /// <summary>
    /// 设备Shell命令
    /// </summary>
    public class ShellCommand : AdbCommand
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <example>
        /// new ShellCommand(device,"ls")
        /// //adb -s device shell "ls"
        /// </example>
        /// <param name="device"></param>
        /// <param name="shellCommand"></param>
        public ShellCommand(IDevice device, string shellCommand) :
            base(device,$"shell \"{shellCommand}\"")
        {
        }
    }
}
