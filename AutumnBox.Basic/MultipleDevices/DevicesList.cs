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

namespace AutumnBox.Basic.MultipleDevices
{
    public class DevicesList : List<DeviceBasicInfo>
    {
        public static DevicesList operator +(DevicesList left, DevicesList right)
        {
            right.ForEach((info) => { left.Add(info); });
            return left;
        }
        public static bool operator ==(DevicesList left, DevicesList right)
        {
            if (left.Count != right.Count) return false;//长度不同就是不同
            try
            {//不包含也是不同
                left.ForEach((i) => { if (!right.Contains(i)) { throw new Exception(); }; });
            }
            catch { return false; }
            return true;
        }
        public static bool operator !=(DevicesList left, DevicesList right)
        {
            return (!(left == right));
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == (DevicesList)obj) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
