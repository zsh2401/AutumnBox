/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:53:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;
using System;
using System.Net;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 网络连接设备
    /// </summary>
    public sealed class NetDevice : DeviceBase
    {
        /// <summary>
        /// IP
        /// </summary>
        public IPEndPoint IPEndPoint { get; internal set; }

        /// <summary>
        /// 网络连接的设备
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="state"></param>
        /// <param name="ipEndPoint"></param>
        public NetDevice(string serialNumber, DeviceState state, IPEndPoint ipEndPoint)
        {
            this.SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
            this.State = state;
            this.IPEndPoint = ipEndPoint;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="ipEndPoint"></param>
        /// <returns></returns>
        public static bool TryParseEndPoint(string serialNumber, out IPEndPoint ipEndPoint)
        {
            try
            {
                var splited = serialNumber.Split(':');
                var ip = IPAddress.Parse(splited[0]);
                var port = ushort.Parse(splited[1]);
                ipEndPoint = new IPEndPoint(ip, port);
                return true;
            }
            catch
            {
                ipEndPoint = null;
                return false;
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="closeNetDebugging"></param>
        public void Disconnect(bool closeNetDebugging = true)
        {
            if (closeNetDebugging)
            {
                new AdbCommand(this, $"usb").Execute()
                .ThrowIfExitCodeNotEqualsZero();
            }
            else
            {
                new AdbCommand($"disconnect {SerialNumber}").Execute()
                .ThrowIfExitCodeNotEqualsZero();
            }
        }
    }
}
