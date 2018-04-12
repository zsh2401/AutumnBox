using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.I18N;
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.UI.CstPanels;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Windows;
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
            TBSdk.Text = string.Format(TBSdk.Text, OpenFramework.BuildInfo.SDK_VERSION);
            this.Loaded += ThridPartyFunctionPanel_Loaded;
            LanguageHelper.LanguageChanged += (s, e) =>
            {
                TBSdk.Text = string.Format(App.Current.Resources["lbApiLevel"].ToString(), OpenFramework.BuildInfo.SDK_VERSION);
            };
        }

        private void ThridPartyFunctionPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LanguageHelper.LanguageChanged += (s, e_) =>
            {
                SetGuiByExtInfomation();
            };
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            ListBoxModule.ItemsSource = ExtensionManager.GetExtensions(App.OpenFrameworkContext);
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

        private void SetGuiByExtInfomation()
        {
            if (ListBoxModule.SelectedIndex != -1)
            {
                GridInfo.Visibility = Visibility.Visible;
                TxtNothing.Visibility = Visibility.Collapsed;
                var ext = ListBoxModule.SelectedItem as IAutumnBoxExtension;
                TBName.Text = ext.Name;
                TBDesc.Text = ext.Infomation;
                SetBtnRunState(ext.RunCheck(new RunCheckArgs(currentDevice)));
            }
            else
            {
                GridInfo.Visibility = Visibility.Collapsed;
                TxtNothing.Visibility = Visibility.Visible;
            }
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetGuiByExtInfomation();
        }

        private FastPanel panel;
        private void ShowRunningBox(IAutumnBoxExtension ext)
        {
            panel = new FastPanel(GridContainer, new ExtensionRuningPanel(ext));
            panel.Display();
        }

        private void CloseRunningBox()
        {
            panel.Close();
        }

        private async void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            var ext = (ListBoxModule.SelectedItem as IAutumnBoxExtension);
            ShowRunningBox(ext);
            await Task.Run(() =>
            {
                ext.Run(new StartArgs()
                {
                    Device = currentDevice
                });
            });
            CloseRunningBox();
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
                BtnRun.Content = App.Current.Resources["btnRunExtension"];
            }
            else
            {
                BtnRun.Content = App.Current.Resources["btnCannotRunExtension"];
            }
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["urlHelpOfInstallExtension"].ToString());
        }

        private void TBDownloadExt_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["urlDownloadExtensions"].ToString());
        }

        private void ButtonDownLoad_Click(object sender, RoutedEventArgs e)
        {
            new DownTagsWindow() { Owner = App.Current.MainWindow }.ShowDialog();
        }
    }
}
