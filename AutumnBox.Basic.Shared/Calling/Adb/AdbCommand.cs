/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:31:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling.Adb
{
    /// <summary>
    /// ADB命令
    /// </summary>
    public class AdbCommand : ProcessBasedCommand
    {
        /// <summary>
        /// 构造一条设备无关的ADB命令
        /// </summary>
        /// <param name="args"></param>
        public AdbCommand(string args) : base(ManagedAdb.Adb.AdbFilePath,$"-P{ManagedAdb.Adb.Server.Port} " + args)
        {
        }
        /// <summary>
        /// 构造一条设备有关的ADB命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="args"></param>
        public AdbCommand(IDevice device, string args) :
            this($"-s {device.SerialNumber} {args}")
        {

        }
        /// <summary>
        /// 在进程开始前对ADB服务进行检查
        /// </summary>
        /// <param name="procStartInfo"></param>
        protected override void BeforeProcessStart(ProcessStartInfo procStartInfo)
        {
            if (!ManagedAdb.Adb.Server.IsEnable)
            {
                throw new InvalidOperationException("adb server is killed");
            }
            base.BeforeProcessStart(procStartInfo);
        }
    }
}
