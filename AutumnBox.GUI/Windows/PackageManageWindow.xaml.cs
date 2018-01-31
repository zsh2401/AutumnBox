using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.UI.FuncPanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutumnBox.GUI.Windows
{
    public class PackageManageWindowApi
    {
        public DeviceSerial Device { get; private set; }
        public Action<string> ShowMessage { get; private set; }
        private readonly Action<bool> _removeMe;
        public PackageManageWindowApi(DeviceSerial device, Action<string> showMesssage, Action<bool> removeCallback)
        {
            this.Device = device;
            this.ShowMessage = showMesssage;
            _removeMe = removeCallback;
        }
        public void RemoveMe(bool deleteSelecetion = true)
        {
            _removeMe(deleteSelecetion);
        }
    }
    /// <summary>
    /// PackageManageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PackageManageWindow : Window
    {

        private readonly DeviceSerial device;
        public PackageManageWindow(DeviceSerial device)
        {
            InitializeComponent();
            this.device = device;
        }

        private List<PackageBasicInfo> CurrentPackages;
        private object locker = new object();
        public void ShowMessage(String message)
        {
            Task.Run(() =>
            {
                lock (locker)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        TBMessage.Text = message;
                        BeginStoryboard((Storyboard)Resources["ShowMessageAnimation"]);
                    });
                    Thread.Sleep(3000);
                    this.Dispatcher.Invoke(() =>
                    {
                        BeginStoryboard((Storyboard)Resources["HideMessageAnimation"]);
                    });
                    Thread.Sleep(500);
                }
            });
        }
        private void RefreshAppList(bool filterSystemApp)
        {
            Task.Run(() =>
            {
                try
                {
                    var pkgs = PackageManager.GetPackages(device);
                    CurrentPackages = (from app in pkgs
                                       where !app.IsSystemApp || filterSystemApp
                                       orderby app.Name
                                       select app).ToList();
                    this.Dispatcher.Invoke(() =>
                    {
                        ListApps.ItemsSource = CurrentPackages;
                    });
                }
                catch (Exception ex)
                {
                    ShowMessage(App.Current.Resources["pmw_msgRefreshFailed"].ToString() + " " + ex.ToString());
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshAppList(CkbShowSystemApp.IsChecked == true);
        }
        private void ListApps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridApplicationPanelContainer.Children.Clear();
            if (ListApps.SelectedIndex >= 0)
            {
                var crtSelect = (PackageBasicInfo)ListApps.SelectedItem;
                GridApplicationPanelContainer.Children.Add(new ApplicationPanel(new PackageManageWindowApi(device, ShowMessage, (deleteSelection) =>
                {
                    GridApplicationPanelContainer.Children.Clear();
                    if (deleteSelection)
                    {
                        RefreshAppList(CkbShowSystemApp.IsChecked == true);
                    }
                }), (PackageBasicInfo)ListApps.SelectedItem));
            }
        }
    }
}
