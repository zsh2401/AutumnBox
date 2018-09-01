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
    /// <summary>
    /// 设备列表
    /// </summary>
    public class DevicesList : List<DeviceBasicInfo>, IEquatable<DevicesList>
    {

        /// <summary>
        /// 检查设备列表是否包含某台设备
        /// </summary>
        /// <param name="devInfo">设备信息</param>
        /// <returns></returns>
        public new bool Contains(DeviceBasicInfo devInfo)
        {
            var haves = from _devInfo in this
                        where _devInfo == devInfo
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
            var tmp = new DevicesList();
            tmp.AddRange(left);
            tmp.AddRange(right);
            return tmp;
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
            return !(left == right);
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

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public override int GetHashCode()
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
        {
            return base.GetHashCode();
        }
    }
}
