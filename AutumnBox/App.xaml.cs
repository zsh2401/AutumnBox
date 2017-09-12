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
        #region 内存回收 http://www.cnblogs.com/xcsn/p/4678322.html
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
        internal static DeviceLink nowLink;//当前的设备连接
        internal static DevicesListener devicesListener = new DevicesListener();//设备监听器
        protected override void OnStartup(StartupEventArgs e)
        {
            new Thread(AutoGC).Start();
            base.OnStartup(e);
        }
        /// <summary>
        /// 无限循环的内存回收方法
        /// </summary>
        private void AutoGC() {
            while (true) {
                ClearMemory();
                Thread.Sleep(1000);
            }
        }
    }
}
