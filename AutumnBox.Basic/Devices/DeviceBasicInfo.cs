/* =============================================================================*\
*
* Filename: DeviceSimpleInfo
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

namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 简单的仅包含设备id和设备状态的结构体,主要用于设备列表 DevicesList
    /// </summary>
    public struct DeviceBasicInfo
    {
        public string Id { get; set; }
        public DeviceStatus Status { get; set; }
        public static implicit operator string(DeviceBasicInfo info)
        {
            return info.Id;
        }
        public static implicit operator DeviceStatus(DeviceBasicInfo info)
        {
            return info.Status;
        }
        public override string ToString()
        {
            return Id;
        }
        public static bool operator ==(DeviceBasicInfo left, DeviceBasicInfo right)
        {
            return (left.Id == right.Id);
        }
        public static bool operator !=(DeviceBasicInfo left, DeviceBasicInfo right)
        {
            return !(left.Id == right.Id);
        }
        /// <summary>
        /// 要不是为了消除警告...
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
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
