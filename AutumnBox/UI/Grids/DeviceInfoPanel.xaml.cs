using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;
using AutumnBox.Helper;
using AutumnBox.Shared;
using AutumnBox.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.UI.Grids
{
    [LogProp(TAG = "DevInfoPanel")]
    /// <summary>
    /// DeviceInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceInfoPanel : UserControl, IDeviceInfoRefreshable
    {
        private static Action<Bitmap, string> _SetStatusPanel;

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
            if (devSimpleInfo.Status == DeviceStatus.RUNNING || devSimpleInfo.Status == DeviceStatus.RECOVERY)
            {
                SetByDeviceSimpleInfo(devSimpleInfo);
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
                _SetStatusPanel(Res.DynamicIcons.no_selected, "PleaseSelectedADevice");
            });
        }
        private void SetByDeviceSimpleInfo(DeviceBasicInfo devSimpleInfo)
        {
            new Thread(() =>
            {
                var simpleInfo = DeviceInfoHelper.GetBuildInfo(devSimpleInfo.Id);
                Logger.D(this, "Get basic info finished");
                this.Dispatcher.Invoke(() =>
                {
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
                        case DeviceStatus.FASTBOOT:
                            _SetStatusPanel(Res.DynamicIcons.fastboot, "DeviceInFastboot");
                            break;
                        case DeviceStatus.RECOVERY:
                            _SetStatusPanel(Res.DynamicIcons.recovery, "DeviceInRecovery");
                            break;
                        case DeviceStatus.RUNNING:
                            _SetStatusPanel(Res.DynamicIcons.poweron, "DeviceInRunning");
                            break;
                        case DeviceStatus.SIDELOAD:
                            _SetStatusPanel(Res.DynamicIcons.recovery, "DeviceInSideload");
                            break;
                    }
                    Logger.D(this, "Finish Base refresh");
                    RefreshFinished?.Invoke(this, new EventArgs());
                });
                var advInfo = DeviceInfoHelper.GetHwInfo(devSimpleInfo.Id);
                bool IsRoot = DeviceInfoHelper.CheckRoot(devSimpleInfo.Id);
                this.Dispatcher.Invoke(() =>
                {
                    LabelRom.Content = (advInfo.StorageTotal != null) ? advInfo.StorageTotal + "GB" : App.Current.Resources["GetFail"].ToString();
                    LabelRam.Content = (advInfo.MemTotal != null) ? advInfo.MemTotal + "GB" : App.Current.Resources["GetFail"].ToString();
                    LabelBattery.Content = (advInfo.BatteryLevel != null) ? advInfo.BatteryLevel + "%" : App.Current.Resources["GetFail"].ToString();
                    LabelSOC.Content = advInfo.SOCInfo ?? App.Current.Resources["GetFail"].ToString();
                    LabelScreen.Content = advInfo.ScreenInfo ?? App.Current.Resources["GetFail"].ToString();
                    LabelFlashMemInfo.Content = advInfo.FlashMemoryType ?? App.Current.Resources["GetFail"].ToString();
                    LabelRootStatus.Content = IsRoot ? App.Current.Resources["RootEnable"].ToString() : App.Current.Resources["RootDisable"].ToString();
                });
            })
            { Name = "Refreshing" }.Start();
            RefreshStart?.Invoke(this, new EventArgs());
        }
        private void GridClick(object sender, MouseButtonEventArgs e)
        {
            Refresh(App.SelectedDevice);
        }
    }
}
