/************************
** auth： zsh2401@163.com
** date:  2017/12/30 12:11:48
** desc： ...
************************/
using System;
using System.Net;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备序列号
    /// </summary>
    public class DeviceSerialNumber : IEquatable<DeviceSerialNumber>
    {
        /// <summary>
        /// 判断这个序列号是否是一个IP
        /// </summary>
        public bool IsIpAdress
        {
            get
            {
                return _ip != null;
            }
        }
        private IPEndPoint _ip = null;
        private string _serialNum = null;
        /// <summary>
        /// 变回string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _serialNum ?? _ip.ToString();
        }
        /// <summary>
        /// 获取类似 -s deviceSerial的字符串
        /// </summary>
        /// <returns></returns>
        public string ToFullSerial() => $"-s {_serialNum ?? _ip.ToString()}";
        /// <summary>
        /// 创建DeviceSerial的新实例
        /// </summary>
        /// <param name="serialStr"></param>
        public DeviceSerialNumber(string serialStr)
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

        /// <summary>
        /// 判断两个serial是否相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DeviceSerialNumber left, DeviceSerialNumber right)
        {
            return left?.ToString() == right?.ToString();
        }
        /// <summary>
        /// 判断两个serial是否不相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DeviceSerialNumber left, DeviceSerialNumber right)
        {
            return left?.ToString() != right?.ToString();
        }
        /// <summary>
        /// 进行比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DeviceSerialNumber other)
        {
            return this.ToString() == other.ToString();
        }
        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DeviceSerialNumber)
            {
                return this.Equals((DeviceSerialNumber)obj);
            }
            else
            {
                return base.Equals(obj);
            }
        }
        /// <summary>
        /// 获取哈希码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
