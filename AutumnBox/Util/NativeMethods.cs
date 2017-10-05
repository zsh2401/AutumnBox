using System;
using System.Runtime.InteropServices;
using static AutumnBox.Helper.BlurHelper;

namespace AutumnBox.Util
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        internal static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
    }
}
