using AutumnBox.Basic.Devices;
using AutumnBox.Images.DynamicIcons;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace AutumnBox
{
    //除基本的按钮点击逻辑外的UI逻辑
    public partial class Window1
    {
        private string _langname = "zh-cn";
        /// <summary>
        /// 更改语言
        /// </summary>
        private void ChangeLanguage()
        {
            if (_langname == "zh-cn")
            {
                Application.Current.Resources.Source = new Uri(@"Lang\en-us.xaml", UriKind.Relative);
                _langname = "en-us";
            }
            else
            {
                Application.Current.Resources.Source = new Uri(@"Lang\zh-cn.xaml", UriKind.Relative);
                _langname = "zh-cn";
            }
        }
        /// <summary>
        /// 根据设备状态改变按钮状态
        /// </summary>
        /// <param name="status"></param>
        private void ChangeButtonByStatus(DeviceStatus status)
        {
            switch (status)
            {
                case DeviceStatus.FASTBOOT:
                    this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(DyanamicIcons.fastboot);
                    this.buttonRebootToBootloader.IsEnabled = true;
                    this.buttonRebootToSystem.IsEnabled = true;
                    this.buttonPushFileToSdcard.IsEnabled = false;
                    this.buttonFlashCustomRecovery.IsEnabled = true;
                    this.buttonRebootToRecovery.IsEnabled = false;
                    break;
                case DeviceStatus.RECOVERY:
                    this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(DyanamicIcons.recovery);
                    this.buttonRebootToBootloader.IsEnabled = true;
                    this.buttonRebootToSystem.IsEnabled = true;
                    this.buttonPushFileToSdcard.IsEnabled = true;
                    this.buttonFlashCustomRecovery.IsEnabled = false;
                    this.buttonRebootToRecovery.IsEnabled = true;
                    break;
                case DeviceStatus.RUNNING:
                    this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(DyanamicIcons.poweron);
                    this.buttonRebootToBootloader.IsEnabled = true;
                    this.buttonRebootToSystem.IsEnabled = true;
                    this.buttonPushFileToSdcard.IsEnabled = true;
                    this.buttonFlashCustomRecovery.IsEnabled = false;
                    this.buttonRebootToRecovery.IsEnabled = true;
                    break;
                default:
                    this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(DyanamicIcons.no_selected);
                    this.buttonRebootToRecovery.IsEnabled = false;
                    this.buttonRebootToBootloader.IsEnabled = false;
                    this.buttonRebootToSystem.IsEnabled = false;
                    this.buttonPushFileToSdcard.IsEnabled = false;
                    this.buttonFlashCustomRecovery.IsEnabled = false;
                    break;
            }
        }

        private delegate void NormalEventHandler();
        private event NormalEventHandler SetUIFinish;
        private void SetUIByDevices() {
            this.Dispatcher.Invoke(new Action(() => {
                DeviceInfo info = core.GetDeviceInfo(this.DevicesListBox.SelectedItem.ToString());
                ChangeButtonByStatus(core.GetDeviceStatus(this.DevicesListBox.SelectedItem.ToString()));
                this.AndroidVersionLabel.Content = info.androidVersion;
                this.CodeLabel.Content = info.code;
                this.ModelLabel.Content = Regex.Replace(info.brand, @"[\r\n]", "") + " " + info.model;
                switch (info.deviceStatus) {
                    case DeviceStatus.RUNNING:
                        this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(DyanamicIcons.poweron);
                        break;
                    case DeviceStatus.FASTBOOT:
                        break;
                    case DeviceStatus.RECOVERY:
                        break;
                    case DeviceStatus.NO_DEVICE:
                        break;
                }
                SetUIFinish?.Invoke();
            }));
        }
    }
}
