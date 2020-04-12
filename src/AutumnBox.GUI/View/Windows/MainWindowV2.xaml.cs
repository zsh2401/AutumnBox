using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.Leafx.Container;
using System;
using System.Windows;
using System.Windows.Interop;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// MainWindowV2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowV2
    {
        public MainWindowV2()
        {
            InitializeComponent();
            App.Current.Lake.Get<IAppLifecycleManager>().AppLoaded += (s, e) =>
             {
                 App.Current.Dispatcher.Invoke(() =>
                 {
                     MenuContainer.Visibility = Visibility.Visible;
                     if (Settings.Default.IsFirstLaunch)
                     {
                         App.Current.Lake.Get<IWindowManager>().Show("Donate");
                     }
                 });
             };
        }
    }
}
