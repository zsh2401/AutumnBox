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
        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;

        //code from https://stackoverflow.com/questions/2032493/install-uninstall-an-inf-driver-programmatically-using-c-sharp-net
        /// <summary>
        /// 调用Setupapi.dll安装驱动
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="ModuleHandle"></param>
        /// <param name="CmdLineBuffer"></param>
        /// <param name="nCmdShow"></param>
        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall)]
        [Obsolete("没什么卵用",true)]
        public static extern void InstallHinfSection(
            [In] IntPtr hwnd,
            [In] IntPtr ModuleHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
            int nCmdShow);
    }
}
