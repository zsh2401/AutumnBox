/* =============================================================================*\
*
* Filename: Main._Refresher.cs
* Description: 
*
* Version: 1.0
* Created: 10/6/2017 03:31:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using AutumnBox.Helper;
using AutumnBox.Util;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Controls;

namespace AutumnBox
{
    /// <summary>
    /// 除基本的按钮点击逻辑外的UI逻辑
    /// </summary>
    public partial class StartWindow
    {
        private Object setUILock = new System.Object();
        /// <summary>
        /// 根据当前选中的设备刷新界面信息
        /// </summary>
        private void RefreshUI()
        {
            lock (setUILock)
            {
                new Thread(() =>
                {
                    SetDeviceInfoLabels();
                    SetAdvanceInfo();
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        SetButtons();
                        //根据状态将图片和按钮状态进行设置
                        switch (App.SelectedDevice.Status)
                        {
                            case DeviceStatus.RUNNING:
                                TabFunctions.SelectedIndex = 1;
                                break;
                            case DeviceStatus.RECOVERY:
                                TabFunctions.SelectedIndex = 2;
                                break;
                            case DeviceStatus.FASTBOOT:
                                TabFunctions.SelectedIndex = 3;
                                break;
                            default:
                                TabFunctions.SelectedIndex = 0;
                                break;
                        }
                        UIHelper.CloseRateBox();
                    }));
                }).Start();
                UIHelper.ShowRateBox();
            }
        }
        /// <summary>
        /// 获取高级信息
        /// </summary>
        private void SetAdvanceInfo()
        {
            if (App.SelectedDevice.Status == DeviceStatus.RECOVERY || App.SelectedDevice.Status == DeviceStatus.RUNNING)
            {
                this.Dispatcher.Invoke(() =>
                {
                    UIHelper.SetGridLabelsContent(GridMemoryInfo, App.Current.Resources["Getting"].ToString());
                    UIHelper.SetGridLabelsContent(GridHardwareInfo, App.Current.Resources["Getting"].ToString());
                });
                new Thread(() =>
                {
                    var info = DevicesHelper.GetDeviceAdvanceInfo(App.SelectedDevice.Id);
                    this.Dispatcher.Invoke(() =>
                    {
                        LabelRom.Content = (info.StorageTotal != null) ? info.StorageTotal + "GB" : App.Current.Resources["GetFail"].ToString();
                        LabelRam.Content = (info.MemTotal != null) ? info.MemTotal + "GB" : App.Current.Resources["GetFail"].ToString();
                        LabelBattery.Content = (info.BatteryLevel != null) ? info.BatteryLevel + "%" : App.Current.Resources["GetFail"].ToString();
                        LabelSOC.Content = (info.SOCInfo != null) ? info.SOCInfo : App.Current.Resources["GetFail"].ToString();
                        LabelScreen.Content = (info.MemTotal != null) ? info.ScreenInfo : App.Current.Resources["GetFail"].ToString();
                        LabelFlashMemInfo.Content = (info.FlashMemoryType != null) ? info.FlashMemoryType : App.Current.Resources["GetFail"].ToString();
                        Logger.D(this,info.IsRoot.ToString());
                        LabelRootStatus.Content = (info.IsRoot) ? App.Current.Resources["RootEnable"].ToString(): App.Current.Resources["RootDisable"].ToString();
                    });
                })
                { Name = "SetAdvanceInfo.." }.Start();
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    UIHelper.SetGridLabelsContent(GridHardwareInfo, "....");
                    UIHelper.SetGridLabelsContent(GridMemoryInfo, "....");
                });
            }
        }
        /// <summary>
        /// 根据设备状态改变按钮状态
        /// </summary>
        /// <param name="status"></param>
        private void SetButtons()
        {
            bool inBootLoader = false;
            bool inRecovery = false;
            bool inRunning = false;
            bool notFound = false;
#pragma warning disable CS0219 // 变量已被赋值，但从未使用过它的值
            bool inSideload = false;
#pragma warning restore CS0219 // 变量已被赋值，但从未使用过它的值
            switch (App.SelectedDevice.Status)
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
            //GridPoweronFuncs.
            UIHelper.SetGridButtonStatus(GridPoweronFuncs._MainGrid, inRunning);
            UIHelper.SetGridButtonStatus(GridRecFuncs, inRecovery);
            UIHelper.SetGridButtonStatus(GridFastbootFuncs, inBootLoader);
            this.buttonRebootToBootloader.IsEnabled = !notFound;
            this.buttonRebootToSystem.IsEnabled = !notFound;
            this.buttonRebootToRecovery.IsEnabled = (inRunning || inRecovery);
        }
        /// <summary>
        /// 根据最新的信息设置设备状态表
        /// </summary>
        private void SetDeviceInfoLabels()
        {
            //AutumnBox.
            Action<Bitmap, string> SetDevInfoImgAndText = (bitmap, key) =>
            {
                this.DeviceStatusImage.Source = UIHelper.BitmapToBitmapImage(bitmap);
                this.DeviceStatusLabel.Content = App.Current.Resources[key].ToString();
            };
            if (App.SelectedDevice.Status == DeviceStatus.NO_DEVICE)
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.AndroidVersionLabel.Content = App.Current.Resources["PleaseSelectedADevice"];
                    this.CodeLabel.Content = App.Current.Resources["PleaseSelectedADevice"];
                    this.ModelLabel.Content = App.Current.Resources["PleaseSelectedADevice"];
                });
            }
            else
            {
                //更改文字
                var info = DevicesHelper.GetDeviceInfo(App.SelectedDevice);
                this.Dispatcher.Invoke(() =>
                {
                    this.AndroidVersionLabel.Content = info.AndroidVersion;
                    this.CodeLabel.Content = info.Code;
                    this.ModelLabel.Content = info.M;
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
                        default:
                            SetDevInfoImgAndText(Res.DynamicIcons.no_selected, "PleaseSelectedADevice");
                            break;
                    }
                });
            }
        }
    }
}
