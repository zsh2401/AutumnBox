/* =============================================================================*\
*
* Filename: NativeMethods.cs
* Description: 
*
* Version: 1.0
* Created: 10/6/2017 04:12:10(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Runtime.InteropServices;
using static AutumnBox.GUI.Util.UI.BlurHelper;

namespace AutumnBox.GUI.Util.OS
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        internal static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        [DllImport("newdev.dll", SetLastError = true)]
        internal static extern bool DiInstallDriver
(
[In] IntPtr hwndParent,
[In] string FullInfPath,
[In] uint Flags,
[Out] bool NeedReboot
);
    }
}
