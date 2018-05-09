/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/16 11:14:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx
{

    public class PoweronFuncsUXImp : IPoweronFuncsUX
    {
        [InstallCheck(BreventServiceActivator._AppPackageName, ErrorMsgKey = "msgPlsInstallBreventFirst")]
        public void ActivateBrevent(DeviceBasicInfo targetDevice)
        {
            bool fixAndroidO = false;
            if (new DeviceBuildPropGetter(targetDevice.Serial).GetAndroidVersion() >= new Version("8.0.0"))
            {
                var result = BoxHelper.ShowChoiceDialog("msgNotice", "msgFixAndroidO", "btnDoNotOpen", "btnOpen");
                switch (result)
                {
                    case ChoiceResult.BtnCancel:
                        return;
                    case ChoiceResult.BtnLeft:
                        fixAndroidO = false;
                        break;
                    case ChoiceResult.BtnRight:
                        fixAndroidO = true;
                        break;
                }
            }
            var args = new ShScriptExecuterArgs() { DevBasicInfo = targetDevice, FixAndroidOAdb = fixAndroidO };
            /*开始操作*/
            BreventServiceActivator activator = new BreventServiceActivator();
            activator.Init(args);
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(IceBoxActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallIceBoxFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateIceBox(DeviceBasicInfo targetDevice)
        {
            IceBoxActivator iceBoxActivator = new IceBoxActivator();
            iceBoxActivator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            iceBoxActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(iceBoxActivator);
        }


        [InstallCheck(AirForzenActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallAirForzenFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateAirForzen(DeviceBasicInfo targetDevice)
        {
            AirForzenActivator airForzenActivator = new AirForzenActivator();
            airForzenActivator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            airForzenActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(airForzenActivator);
        }

        [InstallCheck(ShizukuManagerActivator._AppPackageName, ErrorMsgKey = "msgPlsInstallShizukuManagerFirst")]
        public void ActivateShizukuManager(DeviceBasicInfo targetDevice)
        {
            bool fixAndroidO = false;
            if (new DeviceBuildPropGetter(targetDevice.Serial).GetAndroidVersion() >= new Version("8.0.0"))
            {
                var result = BoxHelper.ShowChoiceDialog("msgNotice", "msgFixAndroidO", "btnDoNotOpen", "btnOpen");
                switch (result)
                {
                    case ChoiceResult.BtnCancel:
                        return;
                    case ChoiceResult.BtnLeft:
                        fixAndroidO = false;
                        break;
                    case ChoiceResult.BtnRight:
                        fixAndroidO = true;
                        break;
                }
            }
            var args = new ShScriptExecuterArgs() { DevBasicInfo = targetDevice, FixAndroidOAdb = fixAndroidO };
            /*开始操作*/
            ShizukuManagerActivator activator = new ShizukuManagerActivator();
            activator.Init(args);
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(IslandActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallAirForzenFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateIsland(DeviceBasicInfo targetDevice)
        {
            IslandActivator islandActivator = new IslandActivator();
            islandActivator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            islandActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(islandActivator);
        }

        [InstallCheck(GeekMemoryCleanerActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallGMCFirst")]
        [Tip("msgActiveGMC")]
        public void ActivateGeekMemoryCleaner(DeviceBasicInfo targetDevice)
        {

            var activator = new GeekMemoryCleanerActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);

        }

        [InstallCheck(StopAppActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallStopAppFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateStopapp(DeviceBasicInfo targetDevice)
        {
            StopAppActivator activator = new StopAppActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(BlackHoleActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallBlackholeFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateBlackHole(DeviceBasicInfo targetDevice)
        {
            BlackHoleActivator activator = new BlackHoleActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(AnzenbokusuActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallAnzenbokusuFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateAnzenbokusu(DeviceBasicInfo targetDevice)
        {
            AnzenbokusuActivator activator = new AnzenbokusuActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(AnzenbokusuFakeActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallAnzenbokusuFakeFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateAnzenbokusuFake(DeviceBasicInfo targetDevice)
        {
            AnzenbokusuFakeActivator activator = new AnzenbokusuFakeActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(FreezeYouActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallFreezeYouFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateFreezeYou(DeviceBasicInfo targetDevice)
        {
            FreezeYouActivator activator = new FreezeYouActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(GreenifyAggressiveDozeActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallGreenifyFirst")]
        public void ActivateGreenifyAggressiveDoze(DeviceBasicInfo targetDevice)
        {
            var activator = new GreenifyAggressiveDozeActivator();
            activator.Init(new FlowArgs()
            {
                DevBasicInfo = targetDevice
            });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [MinAndroidVersion(8)]
        [InstallCheck(UsersirActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallUsersirFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateUsersir(DeviceBasicInfo targetDevice)
        {
            var activator = new UsersirActivator();
            activator.Init(new FlowArgs()
            {
                DevBasicInfo = targetDevice
            });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [NeedRoot]
        [Tip("msgDelScreenLock")]
        public void DeleteScreenLock(DeviceBasicInfo targetDevice)
        {
            var screenLockDeleter = new ScreenLockDeleter();
            screenLockDeleter.Init(new FlowArgs()
            {
                DevBasicInfo = targetDevice,
            });
            screenLockDeleter.RunAsync();
            BoxHelper.ShowLoadingDialog(screenLockDeleter);
        }

        [NeedRoot]
        public void FlashRecovery(DeviceBasicInfo targetDevice)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "镜像文件(*.img)|*.img";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var flasherArgs = new DeviceImageFlasherArgs()
                {
                    DevBasicInfo = targetDevice,
                    ImageType = DeviceImage.Recovery,
                    SourceFile = fileDialog.FileName,
                };
                DeviceImageFlasher flasher = new DeviceImageFlasher();
                flasher.Init(flasherArgs);
                flasher.RunAsync();
                BoxHelper.ShowLoadingDialog(flasher);
            }
        }

        [NeedRoot]
        public void FlashBoot(DeviceBasicInfo targetDevice)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "镜像文件(*.img)|*.img";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var flasherArgs = new DeviceImageFlasherArgs()
                {
                    DevBasicInfo = targetDevice,
                    ImageType = DeviceImage.Boot,
                    SourceFile = fileDialog.FileName,
                };
                DeviceImageFlasher flasher = new DeviceImageFlasher();
                flasher.Init(flasherArgs);
                flasher.RunAsync();
                BoxHelper.ShowLoadingDialog(flasher);
            }
        }

        [NeedRoot]
        public void ExtractRecovery(DeviceBasicInfo targetDevice)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "请选择保存路径"
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;
            var args = new DeviceImageExtractorArgs()
            {
                DevBasicInfo = targetDevice,
                SavePath = fbd.SelectedPath,
                ImageType = DeviceImage.Recovery,
            };
            var extrator = new DeviceImageExtractor();
            extrator.Init(args);
            extrator.RunAsync();
            BoxHelper.ShowLoadingDialog(extrator);
        }
        [NeedRoot]
        public void ExtractBoot(DeviceBasicInfo targetDevice)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "请选择保存路径"
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;
            var args = new DeviceImageExtractorArgs()
            {
                DevBasicInfo = targetDevice,
                SavePath = fbd.SelectedPath,
                ImageType = DeviceImage.Boot,
            };
            var extrator = new DeviceImageExtractor();
            extrator.Init(args);
            extrator.RunAsync();
            BoxHelper.ShowLoadingDialog(extrator);
        }

        public void ChangeDpi(DeviceBasicInfo targetDevice)
        {
            new DpiChangeWindow(targetDevice) { Owner = App.Current.MainWindow }.ShowDialog();
        }

        [NeedRoot]
        [Tip("msgUnlockSystemTip")]
        public void UnlockSystemParation(DeviceBasicInfo targetDevice)
        {
            var unlocker = new SystemPartitionUnlocker();
            unlocker.Init(new FlowArgs()
            {
                DevBasicInfo = targetDevice
            });
            unlocker.RunAsync();
            BoxHelper.ShowLoadingDialog(unlocker);
        }

        public void ScreenShot(DeviceBasicInfo targetDevice)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var shoter = new ScreenShoter();
                shoter.Init(new ScreenShoterArgs()
                {
                    DevBasicInfo = targetDevice,
                    SavePath = fbd.SelectedPath
                });
                shoter.RunAsync();
                BoxHelper.ShowLoadingDialog(shoter);
            }
        }

        public void InstallApk(DeviceBasicInfo targetDevice)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "安卓安装包ApkFile(*.apk)|*.apk";
            fileDialog.Multiselect = true;

            if (fileDialog.ShowDialog() == true)
            {
                ApkInstaller installer = new ApkInstaller();
                List<FileInfo> files = new List<FileInfo>();
                foreach (string fileName in fileDialog.FileNames)
                {
                    files.Add(new FileInfo(fileName));
                }
                var args = new ApkInstallerArgs()
                {
                    DevBasicInfo = targetDevice,
                    Files = files,
                };
                installer.Init(args);
                new ApkInstallingWindow(installer, files).ShowDialog();
            }
            else
            {
                return;
            }
        }

        public void PushFile(DeviceBasicInfo targetDeivce)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "刷机包/压缩包文件(*.zip)|*.zip|镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var args = new FilePusherArgs()
                {
                    DevBasicInfo = targetDeivce,
                    SourceFile = fileDialog.FileName,
                };
                var pusher = new FilePusher();
                pusher.Init(args);
                pusher.MustTiggerAnyFinishedEvent = true;
                pusher.RunAsync();
                new FileSendingWindow(pusher).ShowDialog();
            }
            else
            {
                return;
            }
        }

        [InstallCheck(AppOpsXActivator.ApplicationPackageName, ErrorMsgKey = "msgPlsInstallAppOpsxFirst")]
        public void ActivateAppOpsX(DeviceBasicInfo targetDevice)
        {
            bool fixAndroidO = false;
            if (new DeviceBuildPropGetter(targetDevice.Serial).GetAndroidVersion() >= new Version("8.0.0"))
            {
                var result = BoxHelper.ShowChoiceDialog("msgNotice", "msgFixAndroidO", "btnDoNotOpen", "btnOpen");
                switch (result)
                {
                    case ChoiceResult.BtnCancel:
                        return;
                    case ChoiceResult.BtnLeft:
                        fixAndroidO = false;
                        break;
                    case ChoiceResult.BtnRight:
                        fixAndroidO = true;
                        break;
                }
            }
            var args = new ShScriptExecuterArgs() { DevBasicInfo = targetDevice, FixAndroidOAdb = fixAndroidO };
            /*开始操作*/
            AppOpsXActivator activator = new AppOpsXActivator();
            activator.Init(args);
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        public void BackupDcim(DeviceBasicInfo targetDevice)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = App.Current.Resources["selectDcimBackupFloder"].ToString()
            };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var args = new DcimBackuperArgs()
                {
                    DevBasicInfo = targetDevice,
                    TargetPath = fbd.SelectedPath
                };
                var backuper = new DcimBackuper();
                backuper.Init(args);
                new PullingWindow(backuper) { Owner = App.Current.MainWindow }.Show();
            }
        }
    }
}
