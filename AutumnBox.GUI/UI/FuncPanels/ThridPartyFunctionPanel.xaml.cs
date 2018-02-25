using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.Util;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.V1;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// ThridPartyFunctionPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ThridPartyFunctionPanel : UserControl, IRefreshable
    {
        private DeviceBasicInfo currentDevice;

        public ThridPartyFunctionPanel()
        {
            InitializeComponent();
            GridInfo.Visibility = Visibility.Collapsed;
            TxtNothing.Visibility = Visibility.Visible;
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            currentDevice = deviceSimpleInfo;
            ListBoxModule.ItemsSource = from mod in ExtendModuleManager.GetModules()
                                        where mod.RequiredDeviceState.HasFlag(deviceSimpleInfo.State)
                                        select mod;
        }

        public void Reset()
        {
            currentDevice = new DeviceBasicInfo()
            {
                Serial = null,
                State = DeviceState.None
            };
            ListBoxModule.ItemsSource = null;
            Refresh(currentDevice);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxModule.SelectedIndex != -1)
            {
                GridInfo.Visibility = Visibility.Visible;
                TxtNothing.Visibility = Visibility.Collapsed;
                var module = ListBoxModule.SelectedItem as AutumnBoxExtendModule;
                TBName.Text = module.Name;
                TBDesc.Text = module.Desc;
                TBVersion.Text = module.Version?.ToString();
                TBEmail.Text = module.ContactMail?.ToString();
                TBAuth.Text = module.Auth;
            }
            else
            {
                GridInfo.Visibility = Visibility.Collapsed;
                TxtNothing.Visibility = Visibility.Visible;
            }
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            var module = (ListBoxModule.SelectedItem as AutumnBoxExtendModule);
            try
            {
                module.Run(new RunArgs()
                {
                    Device = currentDevice
                });
            }
            catch (Exception) {
                BoxHelper.ShowMessageDialog("Warning",string.Format(App.Current.Resources["msgModuleFailed"].ToString(), module.Name));
            }
        }

        private void BtnOpenModuleFloder_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(ExtendModuleManager.ModsPath);
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(Urls.INSTALL_MODULE_HELP);
        }
    }
}
