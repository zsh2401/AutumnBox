using AutumnBox.GUI.Services;
using AutumnBox.GUI.Services.Impl.OS;
using AutumnBox.GUI.Util;
using HandyControl.Controls;
using System;
using System.Diagnostics;

namespace AutumnBox.GUI.Views.Windows
{
    /// <summary>
    /// MainWindowV3.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowV3
    {
        public MainWindowV3()
        {
            InitializeComponent();
            this.GetComponent<INotificationManager>().Token = "mainv3";
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                NativeMethods.SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
            }
#if CANARY
                this.GetComponent<INotificationManager>().Info("Canary");
#endif
        }

    }
}
