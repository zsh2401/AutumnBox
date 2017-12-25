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

using AutumnBox.Basic.Connection;
using AutumnBox.Support.CstmDebug;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Devices
{

    /// <summary>
    /// 简单的仅包含设备id和设备状态的结构体,主要用于设备列表 DevicesList
    /// </summary>
    public struct DeviceBasicInfo
    {
        public Serial Serial { get;  set; }
        public DeviceStatus Status { get;  set; }
        public static implicit operator string(DeviceBasicInfo info)
        {
            return info.Serial.ToString();
        }
        public static implicit operator DeviceStatus(DeviceBasicInfo info)
        {
            return info.Status;
        }
        public static DeviceBasicInfo Make(string serialStr, DeviceStatus status)
        {
            return new DeviceBasicInfo
            {
                Serial = new Serial(serialStr),
                Status = status,
            };
        }
        public string ToSampleString() {
            return $"{Serial.ToString()} {Status}";
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
                return this == (DeviceBasicInfo)obj;
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
    }
}
