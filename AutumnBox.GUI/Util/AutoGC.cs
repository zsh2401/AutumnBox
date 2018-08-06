/* =============================================================================*\
*
* Filename: AutoGC
* Description: 
*
* Version: 1.0
* Created: 2017/10/30 11:43:21 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Util
{
    [Obsolete("没什么卵用",true)]
    /// <summary>
    /// AutoGC http://www.cnblogs.com/xcsn/p/4678322.html
    /// </summary>
    internal sealed class AutoGC
    {
        private bool continueAutoGC = true;
        public void Start()
        {
            continueAutoGC = true;
            new Thread(_AutoGC) { Name = "Auto GC" }.Start();
        }
        public void Stop()
        {
            continueAutoGC = false;
        }
        private void _AutoGC()
        {
            while (continueAutoGC)
            {
                ClearMemory();
                Thread.Sleep(1000);
            }
        }
        private void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                NativeMethods.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
    }
}
