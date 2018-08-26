/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 1:38:55 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Adb
{
    public static class AdbProtocol
    {
        public const string STATE_OKAY = "OKAY";
        public const string STATE_FAIL = "FAIL";
        public static byte[] ToAdbRequest(this string request)
        {
            string resultStr = string.Format("{0}{1}\n", request.Length.ToString("X4"), request);
            byte[] result = Encoding.UTF8.GetBytes(resultStr);
            return result;
        }
    }
}
