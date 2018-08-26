/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 2:27:26 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Adb
{
    public static class Extension
    {
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
        public static string ToProtocolString(this AdbResponseState state)
        {
            return state.ToString().ToUpper();
        }
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
        public static AdbResponseState ToAdbResponseState(this byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer).ToAdbResponseState();
        }
    }
}
