/* =============================================================================*\
*
* Filename: DeviceBasicInfo
* Description: 
*
* Version: 1.0
* Created: 2017/10/18 13:03:45(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
namespace AutumnBox.Basic.Device
{

    /// <summary>
    /// 简单的仅包含设备id和设备状态的结构体,主要用于设备列表 DevicesList
    /// </summary>
    public struct DeviceBasicInfo : IEquatable<DeviceBasicInfo>
    {
        /// <summary>
        /// 设备序列号
        /// </summary>
        public DeviceSerialNumber Serial { get; set; }
        /// <summary>
        /// 设备的状态
        /// </summary>
        public DeviceState State { get; set; }
        /// <summary>
        /// 构建一个设备基础信息类
        /// </summary>
        /// <param name="serialStr"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static DeviceBasicInfo Make(string serialStr, DeviceState state)
        {
            return new DeviceBasicInfo
            {
                Serial = new DeviceSerialNumber(serialStr),
                State = state,
            };
        }
        /// <summary>
        /// 获取如 serial state 的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Serial.ToString()} {State}";
        }
        /// <summary>
        /// 比较是否相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DeviceBasicInfo left, DeviceBasicInfo right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// 比较是否不等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DeviceBasicInfo left, DeviceBasicInfo right)
        {
            return !left.Equals(right);
        }
        /// <summary>
        /// 隐式转换为string
        /// </summary>
        /// <param name="info"></param>
        public static implicit operator string(DeviceBasicInfo info)
        {
            return info.ToString();
        }
        /// <summary>
        /// 隐式转换DeviceBasicInfo为Serial
        /// </summary>
        /// <param name="info"></param>
        public static implicit operator DeviceSerialNumber(DeviceBasicInfo info)
        {
            return info.Serial;
        }
        /// <summary>
        /// 隐式转换DeviceBasicInfo为DeviceState
        /// </summary>
        /// <param name="info"></param>
        public static implicit operator DeviceState(DeviceBasicInfo info)
        {
            return info.State;
        }
        /// <summary>
        /// 比较两个DeviceBasicInfo是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DeviceBasicInfo other)
        {
            return this.State == other.State && this.Serial == other.Serial;
        }
        /// <summary>
        /// 比较两个DeviceBasicInfo是否相等
        /// </summary>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DeviceBasicInfo)
            {
                return Equals((DeviceBasicInfo)obj);
            }
            return base.Equals(obj);
        }
        /// <summary>
        /// 获取HashCode()
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
