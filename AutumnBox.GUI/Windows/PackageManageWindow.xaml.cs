using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.GUI.UI.FuncPanels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutumnBox.GUI.Windows
{
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
        private async void RefreshAppList(bool filterSystemApp)
        {

            CurrentPackages = await Task.Run(() =>
            {
                return (from app in PackageManager.GetPackages(device)
                        where !app.IsSystemApp || filterSystemApp
                        orderby app.Name
                        select app).ToList();
            });
            ListApps.ItemsSource = CurrentPackages;
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
                GridApplicationPanelContainer.Children.Add(new ApplicationPanel(device, (PackageBasicInfo)ListApps.SelectedItem, (deleteSelection) =>
                {
                    GridApplicationPanelContainer.Children.Clear();
                    if (deleteSelection)
                    {
                        RefreshAppList(CkbShowSystemApp.IsChecked == true);
                    }
                }));
            }
        }
    }
}
