using AutumnBox.GUI.Services;
using AutumnBox.GUI.Services.Impl.OS;
using AutumnBox.GUI.Util;
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
        }
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.GetComponent<INotificationManager>().Token = "mainv3";
            NativeMethods.SetForegroundWindow(Process.GetCurrentProcess().MainWindowHandle);
        }
    }
}
