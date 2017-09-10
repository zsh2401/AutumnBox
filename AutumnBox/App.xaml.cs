using AutumnBox.Basic.Devices;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace AutumnBox
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                App.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion
        internal static DeviceLink nowLink;
        internal static DevicesListener devicesListener = new DevicesListener();
        protected override void OnStartup(StartupEventArgs e)
        {
            new Thread(AutoGC).Start();
            base.OnStartup(e);
            
        }
        private void AutoGC() {
            while (true) {
                ClearMemory();
                Thread.Sleep(1000);
            }
        }
    }
}
