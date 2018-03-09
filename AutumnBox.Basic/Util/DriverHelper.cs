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
            NativeMethods.InstallHinfSection(IntPtr.Zero, IntPtr.Zero, path, 0);
        }
    }
}
