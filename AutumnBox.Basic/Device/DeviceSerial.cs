/************************
** auth： zsh2401@163.com
** date:  2017/12/30 12:11:48
** desc： ...
************************/
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    public class DeviceSerial : IEquatable<DeviceSerial>
    {
        public bool IsIpAdress
        {
            get
            {
                return _ip != null;
            }
        }
        private IPEndPoint _ip = null;
        private string _serialNum = null;
        public override string ToString()
        {
            return _serialNum ?? _ip.ToString();
        }
        public string ToFullSerial() => $"-s {_serialNum ?? _ip.ToString()}";
        public DeviceSerial(string serialStr)
        {
            var strs = serialStr.Split(':');
            if (strs.Length > 1)
            {
                var ip = IPAddress.Parse(strs[0]);
                var port = Convert.ToInt32(strs[1]);
                _ip = new IPEndPoint(ip, port);
            }
            else
            {
                _serialNum = serialStr;
            }
        }

        public static bool operator ==(DeviceSerial left, DeviceSerial right)
        {
            return left?.ToString() == right?.ToString();
        }
        public static bool operator !=(DeviceSerial left, DeviceSerial right)
        {
            return left?.ToString() != right?.ToString();
        }
        public bool Equals(DeviceSerial other)
        {
            return this.ToString() == other.ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj is DeviceSerial)
            {
                return this.Equals((DeviceSerial)obj);
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
    }
}
