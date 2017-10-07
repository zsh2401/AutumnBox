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
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;

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
            new Thread(()=> {
                lock (setUILock)
                {
                    var info = DevicesHelper.GetDeviceInfo(App.SelectedDevice.Id);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        //根据状态将图片和按钮状态进行设置
                        ChangeButtonByStatus(info.deviceStatus);
                        ChangeImageByStatus(info.deviceStatus);
                        switch (info.deviceStatus)
                        {
                            case DeviceStatus.FASTBOOT:
                                TabFunctions.SelectedIndex = 1;
                                break;
                            default:
                                TabFunctions.SelectedIndex = 0;
                                break;
                        }
                        //更改文字
                        this.AndroidVersionLabel.Content = info.androidVersion;
                        this.CodeLabel.Content = info.code;
                        this.ModelLabel.Content = Regex.Replace(info.brand, @"[\r\n]", "") + " " + info.model;
                        UIHelper.CloseRateBox();
                    }));
                }
            }).Start();
            UIHelper.ShowRateBox(this);
        }
        /// <summary>
        /// 根据设备状态改变按钮状态
        /// </summary>
        /// <param name="status"></param>
        private void ChangeButtonByStatus(DeviceStatus status)
        {
            bool inBootLoader = false;
            bool inRecovery = false;
            bool inRunning = false;
            bool notFound = false;
#pragma warning disable CS0219 // 变量已被赋值，但从未使用过它的值
            bool inSideload = false;
#pragma warning restore CS0219 // 变量已被赋值，但从未使用过它的值
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
            UIHelper.SetGridButtonStatus(GridPoweronFuncs, inRunning);
            UIHelper.SetGridButtonStatus(GridRecFuncs, inRecovery);
            UIHelper.SetGridButtonStatus(GridFastbootFuncs, inBootLoader);
            //this.ButtonMiFlash.IsEnabled = inBootLoader;
            //this.ButtonSideload.IsEnabled = inSideload;
            //this.buttonStartBrventService.IsEnabled = inRunning;
            //this.buttonUnlockMiSystem.IsEnabled = (inRecovery || inRunning);
            //this.buttonRelockMi.IsEnabled = inBootLoader;
            this.buttonRebootToBootloader.IsEnabled = !notFound;
            this.buttonRebootToSystem.IsEnabled = !notFound;
            this.buttonRebootToRecovery.IsEnabled = (inRunning || inRecovery);
            //this.buttonPushFileToSdcard.IsEnabled = (inRecovery || inRunning);
            //this.buttonFlashCustomRecovery.IsEnabled = inBootLoader;
        }
        /// <summary>
        /// 设备状态改变图片
        /// </summary>
        /// <param name="status"></param>
        private void ChangeImageByStatus(DeviceStatus status)
        {
            Action<Bitmap, string> SetDevInfoImgAndText = (bitmap, key) => {
                this.DeviceStatusImage.Source = UIHelper.BitmapToBitmapImage(bitmap);
                this.DeviceStatusLabel.Content = App.Current.Resources[key].ToString();
            };
            switch (status)
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
        }
    }
}
