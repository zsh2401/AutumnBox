/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/29 20:46:08
** filename: ShellReturnCode.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Util
{
    public static class IntToShellReturnCodeExt
    {
        public static LinuxReturnCode ToLinuxReturnCode(this int code)
        {
            switch (code)
            {
                case 0:
                    return LinuxReturnCode.None;
                case 1:
                    return LinuxReturnCode.Error;
                case 127:
                    return LinuxReturnCode.KeyHasExpired;
                default:
                    return LinuxReturnCode.Unknow;
            }

        }
    }
    public enum LinuxReturnCode
    {
        None = 0,
        Error = 1,
        KeyHasExpired = 127,
        Unknow = -1,
    }
}
