using AutumnBox.Basic.Devices;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.I18N;
using AutumnBox.GUI.Resources.Images.DynamicIcons;
using AutumnBox.Support.CstmDebug;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutumnBox.GUI.UI.Grids
{
    [LogProperty(TAG = "DevInfoPanel")]
    /// <summary>
    /// DeviceInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceInfoPanel : UserControl, IDeviceInfoRefreshable, ILogSender
    {
        public bool CurrentDeviceIsRoot { get; private set; }
        private static Action<Bitmap, string> _SetStatusPanel;
        public string LogTag => "DevInfoPanel";
        public bool IsShowLog => true;
        public DeviceInfoPanel()
        {
            InitializeComponent();
            _SetStatusPanel = (bitmap, key) =>
            {
                this.DeviceStatusImage.Source = UIHelper.BitmapToBitmapImage(bitmap);
                this.DeviceStatusLabel.Content = App.Current.Resources[key].ToString();
            };
        }
        public event EventHandler RefreshStart;
        public event EventHandler RefreshFinished;
        public void Refresh(DeviceBasicInfo devSimpleInfo)
        {
            if (devSimpleInfo.Status == DeviceStatus.Poweron || devSimpleInfo.Status == DeviceStatus.Recovery)
            {
                SetByDeviceSimpleInfoAsync(devSimpleInfo);
                RefreshStart?.Invoke(this, new EventArgs());
            }
            else if (devSimpleInfo.Status == DeviceStatus.Unauthorized)
            {
                SetDefault();
                UIHelper.SetGridLabelsContent(GridBuildInfo, App.Current.Resources["PleaseAllowUSBDebug"]);
                _SetStatusPanel(DynamicIcons.no_selected, "PleaseAllowUSBDebug");
            }
            else if (devSimpleInfo.Status == DeviceStatus.Fastboot)
            {
                UIHelper.SetGridLabelsContent(GridBuildInfo, "...");
                UIHelper.SetGridLabelsContent(GridHardwareInfo, "....");
                UIHelper.SetGridLabelsContent(GridMemoryInfo, "....");
                _SetStatusPanel(DynamicIcons.no_selected, "DeviceInFastboot");
            }
            else
            {
                SetDefault();
            }
        }
        public void SetDefault()
        {
            this.Dispatcher.Invoke(() =>
            {
                UIHelper.SetGridLabelsContent(GridBuildInfo, App.Current.Resources["PleaseSelectedADevice"]);
                UIHelper.SetGridLabelsContent(GridHardwareInfo, "....");
                UIHelper.SetGridLabelsContent(GridMemoryInfo, "....");
                _SetStatusPanel(DynamicIcons.no_selected, "PleaseSelectedADevice");
            });
        }
        private async void SetByDeviceSimpleInfoAsync(DeviceBasicInfo devSimpleInfo)
        {
            var simpleInfo = await Task.Run(() =>
            {
                return DeviceInfoHelper.GetBuildInfo(devSimpleInfo.Id);
            });
            LabelAndroidVersion.Content = simpleInfo.AndroidVersion ?? App.Current.Resources["GetFail"].ToString();
            LabelModel.Content = simpleInfo.M ?? App.Current.Resources["GetFail"].ToString();
            LabelCode.Content = simpleInfo.Code ?? App.Current.Resources["GetFail"].ToString();
            LabelRom.Content = App.Current.Resources["Getting"].ToString();
            LabelRam.Content = App.Current.Resources["Getting"].ToString();
            LabelBattery.Content = App.Current.Resources["Getting"].ToString();
            LabelSOC.Content = App.Current.Resources["Getting"].ToString();
            LabelScreen.Content = App.Current.Resources["Getting"].ToString();
            LabelFlashMemInfo.Content = App.Current.Resources["Getting"].ToString();
            LabelRootStatus.Content = App.Current.Resources["Getting"].ToString();
            switch (App.SelectedDevice.Status)
            {
                case DeviceStatus.Fastboot:
                    _SetStatusPanel(DynamicIcons.fastboot, "DeviceInFastboot");
                    break;
                case DeviceStatus.Recovery:
                    _SetStatusPanel(DynamicIcons.recovery, "DeviceInRecovery");
                    break;
                case DeviceStatus.Poweron:
                    _SetStatusPanel(DynamicIcons.poweron, "DeviceInRunning");
                    break;
                case DeviceStatus.Sideload:
                    _SetStatusPanel(DynamicIcons.recovery, "DeviceInSideload");
                    break;
            }
            RefreshFinished?.Invoke(this, new EventArgs());
            var advInfo = await Task.Run(() =>
            {
                return DeviceInfoHelper.GetHwInfo(devSimpleInfo.Id);
            });
            bool IsRoot = DeviceInfoHelper.CheckRoot(devSimpleInfo.Id);
            LabelRom.Content = (advInfo.StorageTotal != null) ? advInfo.StorageTotal + "GB" : App.Current.Resources["GetFail"].ToString();
            LabelRam.Content = (advInfo.MemTotal != null) ? advInfo.MemTotal + "GB" : App.Current.Resources["GetFail"].ToString();
            LabelBattery.Content = (advInfo.BatteryLevel != null) ? advInfo.BatteryLevel + "%" : App.Current.Resources["GetFail"].ToString();
            LabelSOC.Content = advInfo.SOCInfo ?? App.Current.Resources["GetFail"].ToString();
            LabelScreen.Content = advInfo.ScreenInfo ?? App.Current.Resources["GetFail"].ToString();
            LabelFlashMemInfo.Content = advInfo.FlashMemoryType ?? App.Current.Resources["GetFail"].ToString();
            LabelRootStatus.Content = IsRoot ? App.Current.Resources["RootEnable"].ToString() : App.Current.Resources["RootDisable"].ToString();
            CurrentDeviceIsRoot = IsRoot;
        }

        private void LabelAndroidVersion_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LanguageHelper.LanguageChanged += (s, ex) => { Refresh(App.SelectedDevice); };
        }

        private void ImgRefreshDevInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Refresh(App.SelectedDevice);
        }
    }
}
