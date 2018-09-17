/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 2:27:26 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// 拓展
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// 将ADB回应的二进制数据转换为字符串
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string DataAsString(this AdbResponse response)
        {
            try
            {
                return Encoding.UTF8.GetString(response.Data);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 将State转换为协议字符
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string ToProtocolString(this AdbResponseState state)
        {
            return state.ToString().ToUpper();
        }
        /// <summary>
        /// 将ADB传回的状态字符串转换为.NET枚举
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static AdbResponseState ToAdbResponseState(this string str)
        {
            switch (str)
            {
                case "OKAY":
                    return AdbResponseState.Okay;
                case "FAIL":
                    return AdbResponseState.Fail;
                default:
                    return AdbResponseState.Unknown;
            }
        }
        /// <summary>
        /// 将二进制状态数据转换为.NET枚举
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static AdbResponseState ToAdbResponseState(this byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer).ToAdbResponseState();
        }
        /// <summary>
        /// 将字符串转换为典型的ADB请求二进制数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static byte[] ToAdbRequest(this string request)
        {
            string resultStr = string.Format("{0}{1}\n", request.Length.ToString("X4"), request);
            byte[] result = Encoding.UTF8.GetBytes(resultStr);
            return result;
        }
    }
}
