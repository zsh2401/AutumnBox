using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.UI.CstPanels;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.GUI.Util;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Internal;

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
            ListBoxModule.ItemsSource = ExtensionManager.GetExtensions();
            currentDevice = deviceSimpleInfo;
            ListBoxModule.SelectedIndex = -1;
        }

        public void Reset()
        {
            currentDevice = new DeviceBasicInfo()
            {
                Serial = null,
                State = DeviceState.None
            };
            ListBoxModule.SelectedIndex = -1;
            Refresh(currentDevice);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxModule.SelectedIndex != -1)
            {
                GridInfo.Visibility = Visibility.Visible;
                TxtNothing.Visibility = Visibility.Collapsed;
                var rt = ListBoxModule.SelectedItem as ExtensionRuntime;
                TBName.Text = rt.InnerExtension.Name;
                TBDesc.Text = rt.InnerExtension.Description;
                TBVersion.Text = rt.InnerExtension.Version?.ToString();
                TBEmail.Text = rt.InnerExtension.ContactMail?.ToString();
                TBAuth.Text = rt.InnerExtension.Auth;
                if (rt.InnerExtension.RequiredDeviceState.HasFlag(currentDevice.State))
                {
                    BtnRun.IsEnabled = true;
                    BtnRun.Content = App.Current.Resources["btnRunExtension"];
                }
                else
                {
                    BtnRun.IsEnabled = false;
                    BtnRun.Content = App.Current.Resources["btnCannotRunExtension"];
                }

            }
            else
            {
                GridInfo.Visibility = Visibility.Collapsed;
                TxtNothing.Visibility = Visibility.Visible;
            }
        }

        private FastPanel panel;
        private void ShowRunningBox(ExtensionRuntime extRtm)
        {
            panel = new FastPanel(GridContainer, new ExtensionRuningPanel(extRtm));
            panel.Display();
        }

        private void CloseRunningBox()
        {
            panel.Close();
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            var runtime = (ListBoxModule.SelectedItem as ExtensionRuntime);

            try
            {
                ShowRunningBox(runtime);
                runtime.RunAsync(new StartArgs()
                {
                    Device = currentDevice
                }, () => { CloseRunningBox(); });
            }
            catch (Exception)
            {
                BoxHelper.ShowMessageDialog("Warning", string.Format(App.Current.Resources["msgModuleFailed"].ToString(), runtime.InnerExtension.Name));
            }
        }

        private void BtnOpenModuleFloder_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(ExtensionManager.ExtensionsPath);
        }

        private void SetBtnRunState(bool enable)
        {
            BtnRun.IsEnabled = enable;
            if (enable)
            {
                BtnRun.Content = "运行此拓展";
            }
            else
            {
                BtnRun.Content = "当前设备状态下,此拓展无法运行";
            }
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["urlHelpOfInstallExtension"].ToString());
        }
    }
}
