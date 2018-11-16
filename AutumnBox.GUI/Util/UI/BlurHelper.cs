/* =============================================================================*\
*
* Filename: BlurHelper.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 03:14:35(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace AutumnBox.GUI.Util.UI
{
    /// <summary>
    /// 窗体透明帮助类,来自开源代码
    /// </summary>
    internal class BlurHelper
    {
        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_INVALID_STATE = 4
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19
            // ...
        }

        public static void EnableBlur(Window _window)
        {
            var WindowPtr = new WindowInteropHelper(_window).Handle;

            var accent = new AccentPolicy()
            {
                AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
                AccentFlags = 0x20 | 0x40 | 0x80 | 0x100,
            };
            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData()
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };
            //这个类基本废弃,为了尽量少使用本地方法,所以删除了
            //NativeMethods.SetWindowCompositionAttribute(WindowPtr, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }
}
