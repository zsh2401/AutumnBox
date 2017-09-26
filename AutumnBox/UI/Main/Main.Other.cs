using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Functions.RunningManager;
using AutumnBox.Debug;
using AutumnBox.UI;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace AutumnBox
{
    /// <summary>
    /// 除基本的按钮点击逻辑外的UI逻辑
    /// </summary>
    public partial class Window1
    {
        private Object setUILock = new object();
        /// <summary>
        /// 根据设备改变界面,如果按钮状态,显示文字,这个方法需要用新线程来操作.并且完成后将会发生事件
        /// 通过事件可以便可以关闭进度窗
        /// </summary>
        /// <param name="id">设备的id</param>
        private void SetUIByDevices()
        {
            lock (setUILock)
            {
                DeviceSimpleInfo sinfo = new DeviceSimpleInfo();
                this.Dispatcher.Invoke(() =>
                {
                    sinfo = (DeviceSimpleInfo)DevicesListBox.SelectedItem;
                });
                App.nowLink.Reset(sinfo);
                Log.d(TAG, "Getting Device Info");
                DeviceInfo info = App.nowLink.DeviceInfo;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    //根据状态将图片和按钮状态进行设置
                    ChangeButtonAndImageByStatus(info.deviceStatus);
                    //更改文字
                    this.AndroidVersionLabel.Content = info.androidVersion;
                    this.CodeLabel.Content = info.code;
                    this.ModelLabel.Content = Regex.Replace(info.brand, @"[\r\n]", "") + " " + info.model;
                    HideRateBox();
                }));
            }
        }
        /// <summary>
        /// 根据设备状态改变按钮,图片等的状态
        /// </summary>
        /// <param name="status">设备的状态</param>
        private void ChangeButtonAndImageByStatus(DeviceStatus status)
        {
            bool inBootLoader = false;
            bool inRecovery = false;
            bool inRunning = false;
            bool notFound = false;
            bool inSideload = false;
            switch (status)
            {
                case DeviceStatus.FASTBOOT:
                    inBootLoader = true;
                    break;
                case DeviceStatus.RECOVERY:
                    inRecovery = true;
                    break;
                case DeviceStatus.RUNNING:
                    inRunning = true;
                    break;
                case DeviceStatus.SIDELOAD:
                    inSideload = true;
                    break;
                default:
                    notFound = true;
                    break;
            }
            //this.buttonSideload.IsEnabled = (inSideload || inRecovery || inRunning);
            this.buttonStartBrventService.IsEnabled = inRunning;
            this.buttonUnlockMiSystem.IsEnabled = (inRecovery || inRunning);
            this.buttonRelockMi.IsEnabled = inBootLoader;
            this.buttonRebootToBootloader.IsEnabled = !notFound;
            this.buttonRebootToSystem.IsEnabled = !notFound;
            this.buttonRebootToRecovery.IsEnabled = (inRunning || inRecovery);
            this.buttonPushFileToSdcard.IsEnabled = (inRecovery || inRunning);
            this.buttonFlashCustomRecovery.IsEnabled = inBootLoader;
            switch (status)
            {
                case DeviceStatus.FASTBOOT:
                    this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(Res.DynamicIcons.fastboot);
                    this.DeviceStatusLabel.Content = FindResource("DeviceInFastboot").ToString();
                    break;
                case DeviceStatus.RECOVERY:
                    this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(Res.DynamicIcons.recovery);
                    this.DeviceStatusLabel.Content = FindResource("DeviceInRecovery").ToString();
                    break;
                case DeviceStatus.RUNNING:
                    this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(Res.DynamicIcons.poweron);
                    this.DeviceStatusLabel.Content = FindResource("DeviceInRunning").ToString();
                    break;
                case DeviceStatus.SIDELOAD:
                    this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(Res.DynamicIcons.recovery);
                    this.DeviceStatusLabel.Content = FindResource("DeviceInSideload").ToString();
                    break;
                case DeviceStatus.DEBUGGING_DEVICE:
                    this.DeviceStatusLabel.Content = FindResource("DeviceIsDebugging").ToString();
                    break;
                default:
                    this.DeviceStatusImage.Source = Tools.BitmapToBitmapImage(Res.DynamicIcons.no_selected);
                    this.DeviceStatusLabel.Content = FindResource("PleaseSelectedADevice").ToString();
                    break;
            }
        }
        /// <summary>
        /// 通过此方法来显示进度框可以确保不会同时出现多个进度窗口
        /// </summary>
        private void ShowRateBox(RunningManager rm = null)
        {
            try
            {
                if (rm == null)
                {
                    rateBox = new RateBox(this);
                    rateBox.ShowDialog();
                    return;
                }
                if (rateBox.IsActive) rateBox.Close();
                rateBox = new RateBox(this, rm);
                rateBox.ShowDialog();
            }
            catch
            {
                this.rateBox = new RateBox(this, rm);
                rateBox.ShowDialog();
            }
        }
        /// <summary>
        /// 隐藏进度窗口
        /// </summary>
        private void HideRateBox()
        {
            try
            {
                this.rateBox.Close();
            }
            catch { }
        }
    }
}
