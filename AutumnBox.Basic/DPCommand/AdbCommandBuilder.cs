/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 2:34:16 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.DPCommand
{
    /// <summary>
    /// 通用命令构造器
    /// </summary>
    public sealed class AdbCommandBuilder : CommandBuilder
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AdbCommandBuilder()
        {
            FileName = AdbProtocol.ADB_EXECUTABLE_FILE_PATH;
            Arguments.Add("-P " + AdbServer.Instance.Port);
        }
        /// <summary>
        /// 清空
        /// </summary>
        public override void ClearArgs()
        {
            base.ClearArgs();
            Arg("-P " + AdbServer.Instance.Port);
        }
        /// <summary>
        /// 设置文件,然而AdbCommandBuilder不可以这么做,内部钦定是adb.exe了
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public override CommandBuilder File(string fileName)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// 设置设备
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public AdbCommandBuilder Device(string serialNumber)
        {
            serialNumber.ThrowIfNullArg();
            Arguments.Add("-s " + serialNumber);
            return this;
        }
        /// <summary>
        /// 设置设备
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public AdbCommandBuilder Device(IDevice device)
        {
            device.ThrowIfNullArg();
            Arguments.Add("-s " + device.SerialNumber);
            return this;
        }
    }
}
