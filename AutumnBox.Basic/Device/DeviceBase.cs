/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:54:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using System.Collections.Generic;
using System.Text;

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
        /// Product
        /// </summary>
        public string Product { get; internal protected set; }

        /// <summary>
        /// Model
        /// </summary>
        public string Model { get; internal protected set; }

        /// <summary>
        /// TransportId
        /// </summary>
        public string TransportId { get; internal protected set; }
        /// <summary>
        /// State
        /// </summary>
        public DeviceState State { get; internal protected set; }

        /// <summary>
        /// 检测是否存活
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return this.Adb("get-state").Item2 == 0;
            }
        }

        /// <summary>
        /// 对比
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IDevice other)
        {
            return other != null && other.SerialNumber == this.SerialNumber;
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

        private const string STATE_PATTERN = "";
        /// <summary>
        /// 刷新设备状态
        /// </summary>
        public void RefreshState()
        {
            var exeResult = this.Adb("get-state");
            exeResult.ThrowIfExitCodeNotEqualsZero();
            var stateString = exeResult.Item1.ToString().Trim();
            this.State = stateString.ToDeviceState();
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

        public override string ToString()
        {
            return $"{SerialNumber}-{State.ToString().ToLower()}";
        }
    }
}
