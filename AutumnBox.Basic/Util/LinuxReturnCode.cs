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
    /// <summary>
    /// Linux执行返回码
    /// </summary>
    public enum LinuxReturnCode
    {
        /// <summary>
        /// 没事
        /// </summary>
        None = 0,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 1,
        /// <summary>
        /// KeyHasExpired
        /// </summary>
        KeyHasExpired = 127,
        /// <summary>
        /// 未知
        /// </summary>
        Unknow = -1,
    }
}
