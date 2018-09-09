/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 16:12:20 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling.Fastboot
{
    /// <summary>
    /// Fastboot命令
    /// </summary>
    public class FastbootCommand : ProcessBasedCommand
    {
        /// <summary>
        /// Fastboot命令
        /// </summary>
        /// <param name="args"></param>
        public FastbootCommand(string args) 
            : base(ManagedAdb.Adb.FastbootFilePath, args)
        {
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        public FastbootCommand(IDevice device, string args) :
            base(ManagedAdb.Adb.FastbootFilePath, $"-s {device.SerialNumber} {args}")
        {

        }
    }
}
