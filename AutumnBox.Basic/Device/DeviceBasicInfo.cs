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
        public Serial Serial { get; set; }
        public DeviceState State { get; set; }
        public static implicit operator string(DeviceBasicInfo info)
        {
            return info.Serial.ToString();
        }
        public static implicit operator DeviceState(DeviceBasicInfo info)
        {
            return info.State;
        }
        public static DeviceBasicInfo Make(string serialStr, DeviceState state)
        {
            return new DeviceBasicInfo
            {
                Serial = new Serial(serialStr),
                State = state,
            };
        }
        public string ToSampleString()
        {
            return $"{Serial.ToString()} {State}";
        }
        public override string ToString()
        {
            return Serial.ToString();
        }
        public static bool operator ==(DeviceBasicInfo left, DeviceBasicInfo right)
        {
            return (left.Serial.ToString() == right.Serial.ToString());
        }
        public static bool operator !=(DeviceBasicInfo left, DeviceBasicInfo right)
        {
            return left.Serial.ToString() != right.Serial.ToString();
        }
        /// <summary>
        /// 要不是为了消除警告...
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DeviceBasicInfo)
            {
                return Equals((DeviceBasicInfo)obj);
            }
            else
            {
                return base.Equals(obj);
            }
        }
        /// <summary>
        /// 要不是为了消除警告...
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(DeviceBasicInfo other)
        {
            return this.State == other.State && this.Serial == other.Serial;
        }
    }
}
