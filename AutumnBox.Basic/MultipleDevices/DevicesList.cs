/* =============================================================================*\
*
* Filename: DevicesList.cs
* Description: 
*
* Version: 1.0
* Created: 9/16/2017 03:48:19(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.Basic.MultipleDevices
{
    public class DevicesList : List<DeviceBasicInfo>, IEquatable<DevicesList>
    {

        /// <summary>
        /// 检查设备列表是否包含某台设备
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public bool Contains(DeviceSerial serial)
        {
            var haves = from _devInfo in this
                        where _devInfo.Serial == serial
                        select _devInfo;
            return haves.Count() > 0;
        }
        /// <summary>
        /// 将设备列表进行合并
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static DevicesList operator +(DevicesList left, DevicesList right)
        {
            left.AddRange(right);
            return left;
        }
        /// <summary>
        /// 判断两个设备列表的内容是否一致
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DevicesList left, DevicesList right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// 判断两个设备列表的内容是否不一致
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DevicesList left, DevicesList right)
        {
            return !left.Equals(right);
        }
        /// <summary>
        /// 判断列表内容是否一致
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DevicesList other)
        {
            if (Count != other.Count) return false;
            foreach (DeviceBasicInfo info in this)
            {
                if (!other.Contains(info)) return false;
            }
            return true;
        }
        /// <summary>
        /// 判断内容是否一致
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DevicesList)
            {
                return base.Equals((DevicesList)obj);
            }
            else
            {
                return base.Equals(obj);
            }
        }
        /// <summary>
        /// 获取hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            long hashCode = 0;
            this.ForEach((info) =>
            {
                hashCode += info.ToString().GetHashCode();
            });
            return (int)(hashCode / this.Count);
        }
    }
}
