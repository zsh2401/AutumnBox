/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/30 22:18:08 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Util
{
    internal static class DriverHelper
    {
        public static void InstallDriver(string path) {
            InstallHinfSection(IntPtr.Zero, IntPtr.Zero, path, 0);
        }
        //code from https://stackoverflow.com/questions/2032493/install-uninstall-an-inf-driver-programmatically-using-c-sharp-net
        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall)]
        public static extern void InstallHinfSection(
            [In] IntPtr hwnd,
            [In] IntPtr ModuleHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
            int nCmdShow);
    }
}
