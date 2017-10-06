/* =============================================================================*\
*
* Filename: App.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 7/31/2017 05:34:44(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions.RunningManager;
using AutumnBox.Util;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;

namespace AutumnBox
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal static Window OwnerWindow { get; private set; }
        internal static DeviceSimpleInfo SelectedDevice;
        internal static DevicesMonitor devicesListener = new DevicesMonitor();//设备监听器
        protected override void OnStartup(StartupEventArgs e)
        {
            new Thread(AutoGC) { Name = "Auto GC..." }.Start();
            base.OnStartup(e);
        }
        protected override void OnLoadCompleted(NavigationEventArgs e)
        {
            base.OnLoadCompleted(e);
            OwnerWindow = this.MainWindow;
        }
        /// <summary>
        /// 无限循环的内存回收方法
        /// </summary>
        private void AutoGC()
        {
            while (true)
            {
                ClearMemory();
                Thread.Sleep(1000);
            }
        }
        #region 内存回收 http://www.cnblogs.com/xcsn/p/4678322.html
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
               NativeMethods.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion
    }
}
