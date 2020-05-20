/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:54:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 基础的设备实现类
    /// </summary>
    public abstract class DeviceBase : IDevice
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; internal protected set; }

        /// <summary>
        /// State
        /// </summary>
        public DeviceState State { get; internal protected set; }

        /// <summary>
        /// 检测是否存活
        /// </summary>
        public bool IsAlive()
        {
            return State == DeviceState.Fastboot || this.Adb("get-state").ExitCode == 0;
        }

        /// <summary>
        /// 对比
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IDevice other)
        {
            return other != null && other.SerialNumber == this.SerialNumber && other.State == this.State;
        }

        /// <summary>
        /// 对比
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as IDevice);
        }

        /// <summary>
        /// 获取Hash码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hashCode = 927976858;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SerialNumber);
            return hashCode;
        }

        /// <summary>
        /// 刷新状态
        /// </summary>
        public void RefreshState()
        {
            var exeResult = this.Adb("get-state");
            try
            {
                exeResult.ThrowIfExitCodeNotEqualsZero();
                var stateString = exeResult.Output.ToString().Trim();
                State = stateString.ToDeviceState();
            }
            catch
            {
                State = DeviceState.Offline;
            }
        }

        /// <summary>
        /// ==
        /// </summary>
        /// <param name="base1"></param>
        /// <param name="base2"></param>
        /// <returns></returns>
        public static bool operator ==(DeviceBase base1, DeviceBase base2)
        {
            return EqualityComparer<DeviceBase>.Default.Equals(base1, base2);
        }

        /// <summary>
        /// !=
        /// </summary>
        /// <param name="base1"></param>
        /// <param name="base2"></param>
        /// <returns></returns>
        public static bool operator !=(DeviceBase base1, DeviceBase base2)
        {
            return !(base1 == base2);
        }

        /// <summary>
        /// 字符串化
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{SerialNumber}-{State.ToString().ToLower()}";
        }


        /// <summary>
        /// 尝试解析一行设备信息字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="dev"></param>
        /// <returns></returns>
        public static bool TryParse(string input, out IDevice dev)
        {
            if (!TryGetBasicInfo(input, out string serialNumber, out DeviceState state))
            {
                dev = null;
                return false;
            }
            if (NetDevice.TryParseEndPoint(serialNumber, out IPEndPoint endPoint))
            {
                dev = new NetDevice(serialNumber, state, endPoint);
                return true;
            }
            else
            {
                dev = new UsbDevice(serialNumber, state);
                return true;
            }
        }

        private const string DEVICES_PATTERN = @"(?i)^(?<sn>[^\u0020|^\t]+)[^\w]+(?<state>\w+)[^\w+]?$";
        private static readonly Regex _deviceRegex = new Regex(DEVICES_PATTERN, RegexOptions.Compiled);
        /// <summary>
        /// 尝试从一行设备信息字符串中获取信息
        /// </summary>
        /// <param name="input"></param>
        /// <param name="serialNumber"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool TryGetBasicInfo(string input, out string serialNumber, out DeviceState state)
        {
            var match = _deviceRegex.Match(input);
            if (!match.Success)
            {
                serialNumber = null;
                state = default(DeviceState);
                return false;
            }
            else
            {
                serialNumber = match.Result("${sn}");
                state = match.Result("${state}").Trim().ToDeviceState();
                return true;
            }
        }
    }
}
