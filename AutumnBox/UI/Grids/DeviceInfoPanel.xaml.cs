using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;
using AutumnBox.Helper;
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
    /// <summary>
    /// DeviceInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceInfoPanel : UserControl, IRefreshable
    {
        Action<Bitmap, string> SetDevInfoImgAndText;
        public Action<object> RefreshStart { get; set; }
        public Action<object> RefreshFinished { get; set; }

        public DeviceInfoPanel()
        {
            InitializeComponent();
            SetDevInfoImgAndText = (bitmap, key) =>
            {
                this.DeviceStatusImage.Source = UIHelper.BitmapToBitmapImage(bitmap);
                this.DeviceStatusLabel.Content = App.Current.Resources[key].ToString();
            };
        }
        public void Refresh() {
            Refresh(App.SelectedDevice);
        }
        public void Refresh(DeviceSimpleInfo devSimpleInfo)
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
        private void SetDefault()
        {
            this.Dispatcher.Invoke(() =>
            {
                UIHelper.SetGridLabelsContent(GridBuildInfo, App.Current.Resources["PleaseSelectedADevice"]);
                UIHelper.SetGridLabelsContent(GridHardwareInfo, "....");
                UIHelper.SetGridLabelsContent(GridMemoryInfo, "....");
                SetDevInfoImgAndText(Res.DynamicIcons.no_selected, "PleaseSelectedADevice");
            });
        }
        private void SetByDeviceSimpleInfo(DeviceSimpleInfo devSimpleInfo)
        {
            new Thread(() =>
            {
                var simpleInfo = DevicesHelper.GetDeviceInfo(devSimpleInfo.Id);
                var advInfo = DevicesHelper.GetDeviceAdvanceInfo(devSimpleInfo.Id);
                this.Dispatcher.Invoke(() =>
                {
                    LabelRom.Content = (advInfo.StorageTotal != null) ? advInfo.StorageTotal + "GB" : App.Current.Resources["GetFail"].ToString();
                    LabelRam.Content = (advInfo.MemTotal != null) ? advInfo.MemTotal + "GB" : App.Current.Resources["GetFail"].ToString();
                    LabelBattery.Content = (advInfo.BatteryLevel != null) ? advInfo.BatteryLevel + "%" : App.Current.Resources["GetFail"].ToString();
                    LabelSOC.Content = advInfo.SOCInfo ?? App.Current.Resources["GetFail"].ToString();
                    LabelScreen.Content = advInfo.ScreenInfo ?? App.Current.Resources["GetFail"].ToString();
                    LabelFlashMemInfo.Content = advInfo.FlashMemoryType ?? App.Current.Resources["GetFail"].ToString();
                    LabelRootStatus.Content = (advInfo.IsRoot) ? App.Current.Resources["RootEnable"].ToString() : App.Current.Resources["RootDisable"].ToString();
                    LabelAndroidVersion.Content = simpleInfo.AndroidVersion;
                    LabelModel.Content = simpleInfo.M;
                    LabelCode.Content = simpleInfo.Code;
                    switch (App.SelectedDevice.Status)
                    {
                        case DeviceStatus.FASTBOOT:
                            SetDevInfoImgAndText(Res.DynamicIcons.fastboot, "DeviceInFastboot");
                            break;
                        case DeviceStatus.RECOVERY:
                            SetDevInfoImgAndText(Res.DynamicIcons.recovery, "DeviceInRecovery");
                            break;
                        case DeviceStatus.RUNNING:
                            SetDevInfoImgAndText(Res.DynamicIcons.poweron, "DeviceInRunning");
                            break;
                        case DeviceStatus.SIDELOAD:
                            SetDevInfoImgAndText(Res.DynamicIcons.recovery, "DeviceInSideload");
                            break;
                    }
                });
                RefreshFinished?.Invoke(this);
            })
            { Name = "Refreshing" }.Start();
            RefreshStart?.Invoke(this);
        }

        private void GridClick(object sender, MouseButtonEventArgs e)
        {
            Refresh(App.SelectedDevice);
        }
    }
}
