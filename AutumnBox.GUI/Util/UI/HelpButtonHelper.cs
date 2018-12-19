using AutumnBox.GUI.Util.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AutumnBox.GUI.Util.UI
{
    static class HelpButtonHelper
    {
        private const uint WS_EX_CONTEXTHELP = 0x00000400;
        private const uint WS_MINIMIZEBOX = 0x00020000;
        private const uint WS_MAXIMIZEBOX = 0x00010000;
        private const int GWL_STYLE = -16;
        private const int GWL_EXSTYLE = -20;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_FRAMECHANGED = 0x0020;
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_CONTEXTHELP = 0xF180;

        public static void EnableHelpButton(this Window window, Action onClickAction)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (onClickAction == null)
            {
                throw new ArgumentNullException(nameof(onClickAction));
            }
            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            uint styles = NativeMethods.GetWindowLong(hwnd, GWL_STYLE);
            styles &= 0xFFFFFFFF ^ (WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
            NativeMethods.SetWindowLong(hwnd, GWL_STYLE, styles);
            styles = NativeMethods.GetWindowLong(hwnd, GWL_EXSTYLE);
            styles |= WS_EX_CONTEXTHELP;
            NativeMethods.SetWindowLong(hwnd, GWL_EXSTYLE, styles);
            NativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
            ((HwndSource)PresentationSource.FromVisual(window)).AddHook(new HookClass(onClickAction).MainHook);
        }
        private class HookClass
        {
            private readonly Action handling;

            public HookClass(Action handling)
            {
                this.handling = handling;
            }

            public IntPtr MainHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                if (msg == WM_SYSCOMMAND &&
                        ((int)wParam & 0xFFF0) == SC_CONTEXTHELP)
                {
                    handling();
                    handled = true;
                }
                return IntPtr.Zero;
            }
        }

    }
}
