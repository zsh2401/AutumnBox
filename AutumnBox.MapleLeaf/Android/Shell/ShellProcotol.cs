/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 20:05:05 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AutumnBox.MapleLeaf.Android.Shell
{
    public static class ShellProcotol
    {
        //[MarshalAs(UnmanagedType.ByValArray)]

        public static byte[] ToStdInput(this string input)
        {
            byte[] commandBytes = Encoding.UTF8.GetBytes(input + "\n");
            byte[] lenBytes = BitConverter.GetBytes(commandBytes.Length);
            Array.Reverse(lenBytes);
            byte[] buffer = new byte[5 + commandBytes.Length];

            Array.Copy(lenBytes, 0, buffer, 1, 4);
            Array.Copy(commandBytes, 0, buffer, 5, commandBytes.Length);
            return buffer;
        }
    }
}
