/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/9 13:37:24 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Runtime.InteropServices;

namespace AutumnBox.Basic.Util
{
    internal static class NativeMethods
    {
        //code from https://stackoverflow.com/questions/2032493/install-uninstall-an-inf-driver-programmatically-using-c-sharp-net
        /// <summary>
        /// 调用Setupapi.dll安装驱动
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="ModuleHandle"></param>
        /// <param name="CmdLineBuffer"></param>
        /// <param name="nCmdShow"></param>
        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall)]
        public static extern void InstallHinfSection(
            [In] IntPtr hwnd,
            [In] IntPtr ModuleHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
            int nCmdShow);
    }
}
