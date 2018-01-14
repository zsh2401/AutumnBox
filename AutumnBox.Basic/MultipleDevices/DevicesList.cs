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
        public bool Contains(Serial serial)
        {
            var haves = from _devInfo in this
                        where _devInfo.Serial == serial
                        select _devInfo;
            return haves.Count() > 0;
        }
        public static DevicesList operator +(DevicesList left, DevicesList right)
        {
            right.ForEach((info) => { left.Add(info); });
            return left;
        }
        public static bool operator ==(DevicesList left, DevicesList right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(DevicesList left, DevicesList right)
        {
            return !left.Equals(right);
        }
        public bool Equals(DevicesList other)
        {
            if (this.Count != other.Count) return false;
            try
            {
                this.ForEach((deviceInfo) =>
                {
                    if (!other.Contains(deviceInfo)) throw new DeviceNotFoundOnEqualingException();
                });
                return true;
            }
            catch (DeviceNotFoundOnEqualingException)
            {
                return false;
            }
        }
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
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        private class DeviceNotFoundOnEqualingException : Exception { }
    }
}
