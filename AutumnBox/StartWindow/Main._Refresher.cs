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
            new Thread(() =>
            {
                lock (setUILock)
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
                                Logger.D(this, "Change selectedIndex to 1");
                                TabFunctions.SelectedIndex = 1;
                                break;
                            case DeviceStatus.RECOVERY:
                                Logger.D(this, "Change selectedIndex to 2");
                                TabFunctions.SelectedIndex = 2;
                                break;
                            case DeviceStatus.FASTBOOT:
                                Logger.D(this, "Change selectedIndex to 3");
                                TabFunctions.SelectedIndex = 3;
                                break;
                            default:
                                Logger.D(this, "Change selectedIndex to 0");
                                TabFunctions.SelectedIndex = 0;
                                break;
                        }
                        UIHelper.CloseRateBox();
                    }));
                }
            }).Start();
            UIHelper.ShowRateBox(this);
        }
        private void SetAdvanceInfo()
        {
            if (App.SelectedDevice.Status == DeviceStatus.RECOVERY || App.SelectedDevice.Status == DeviceStatus.RUNNING)
            {
                new Thread(() =>
                {
                    Action<Label, int, string> setInt = (label, content, unit) =>
                    {
                        if (content != 0) label.Content = content.ToString() + unit;
                        else label.Content = App.Current.Resources["GetFail"];
                    };
                    Action<Label, double, string> setDouble = (label, content, unit) =>
                    {
                        if (content != 0.0) label.Content = content.ToString() + unit;
                        else label.Content = App.Current.Resources["GetFail"];
                    };
                    Action<Label, string> setString = (label, content) =>
                    {
                        if (content != String.Empty) label.Content = content.ToString();
                        else label.Content = App.Current.Resources["GetFail"];
                    };
                    this.Dispatcher.Invoke(() =>
                    {
                        UIHelper.SetGridLabelsContent(GridMemoryInfo, App.Current.Resources["Getting"].ToString());
                        UIHelper.SetGridLabelsContent(GridHardwareInfo, App.Current.Resources["Getting"].ToString());
                    });
                    var info = DevicesHelper.GetDeviceAdvanceInfo(App.SelectedDevice.Id);
                    this.Dispatcher.Invoke(() =>
                    {
                        setDouble(LabelRom, info.StorageTotal, "GB");
                        setDouble(LabelRam, info.MemTotal, "GB");
                        setInt(LabelBattery, info.BatteryLevel, "%");
                        setInt(LabelRom, info.StorageTotal, "GB");
                        setString(LabelSOC, info.SOCInfo);
                        setString(LabelScreen, info.ScreenInfo);
                        setString(LabelFlashMemInfo, info.FlashMemoryType);
                    });
                })
                { Name = "SetAdvanceInfo.." }.Start();
            }
            else {
                this.Dispatcher.Invoke(()=> {
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
            UIHelper.SetGridButtonStatus(GridPoweronFuncs, inRunning);
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
                    this.AndroidVersionLabel.Content = info.androidVersion;
                    this.CodeLabel.Content = info.code;
                    this.ModelLabel.Content = info.m;
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
