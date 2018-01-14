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
        public override string ToString()
        {
            return $"{Serial.ToString()} {State}";
        }
        public static bool operator ==(DeviceBasicInfo left, DeviceBasicInfo right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(DeviceBasicInfo left, DeviceBasicInfo right)
        {
            return !left.Equals(right);
        }

        public bool Equals(DeviceBasicInfo other)
        {
            return this.State == other.State && this.Serial == other.Serial;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
