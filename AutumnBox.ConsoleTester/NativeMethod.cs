/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/20 22:18:04 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester
{
    public static class NativeMethod
    {
        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", SetLastError =true, CallingConvention = CallingConvention.Winapi)]
        public static extern void InstallHinfSection(
     [In] IntPtr hwnd,
     [In] IntPtr ModuleHandle,
     [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
     int nCmdShow);
        [DllImport("newdev.dll",SetLastError =true)]
        public static extern bool DiInstallDriver
(
    [In] IntPtr hwndParent,
    [In] string FullInfPath,
    [In] uint Flags,
    [Out] bool NeedReboot
);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint GetLastError();
    }
}
