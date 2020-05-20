#nullable enable
/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:53:10 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.Util;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 通过USB连接的设备
    /// </summary>
    public sealed class UsbDevice : DeviceBase
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="state"></param>
        public UsbDevice(string serialNumber, DeviceState state)
        {
            this.SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
            this.State = state;
        }

        /// <summary>
        /// 开启网络调试
        /// </summary>
        /// <param name="port">端口号</param>
        /// <exception cref="AdbCommandFailedException">无法执行命令</exception>
        /// <returns>该设备网络地址，如果获取成功则为值，否则为null</returns>
        public IPEndPoint? OpenNetDebugging(ushort port)
        {
            IPAddress? ip = null;
            try
            {
                ip = this.GetLanIP();
            }
            catch { }
            this.Adb($"tcpip {port}").ThrowIfExitCodeNotEqualsZero();
            if (ip != null)
            {
                return new IPEndPoint(ip, port);
            }
            else
            {
                return null;
            }
        }
    }
}
