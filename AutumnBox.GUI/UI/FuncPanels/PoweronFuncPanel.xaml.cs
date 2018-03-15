using System;
using System.Windows;
using System.Windows.Forms;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using System.Threading.Tasks;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows;
using System.IO;
using System.Collections.Generic;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.Basic.Device.PackageManage;
using KingAOP.Aspects;
using System.Dynamic;
using System.Linq.Expressions;
using KingAOP;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// PoweronFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class PoweronFuncPanel : FastPanelChild, IRefreshable
    {

        public PoweronFuncPanel()
        {
            InitializeComponent();
        }
        private DeviceBasicInfo _currentDevInfo;
        public void Reset()
        {
            UIHelper.SetGridButtonStatus(MainGrid, false);
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            this._currentDevInfo = deviceSimpleInfo;
            bool status = deviceSimpleInfo.State == DeviceState.Poweron;
            UIHelper.SetGridButtonStatus(MainGrid, status);
        }

        private bool BreventPrecheck(DeviceSerialNumber seria, string pkgName, string msgOnNotInstall, ref bool fixAndroidO)
        {
            bool? isInstall = false;
            bool isAndroidO = false;
            Task.Run(() =>
            {
                isInstall = new DeviceSoftwareInfoGetter(_currentDevInfo.Serial).IsInstall(pkgName);
                try
                {
                    Version currentDevAndroidVersion = new DeviceBuildPropGetter(_currentDevInfo.Serial).GetAndroidVersion();
                    isAndroidO = currentDevAndroidVersion >= new Version("8.0");
                }
                catch (NullReferenceException) { }
                this.Dispatcher.Invoke(() =>
                {
                    BoxHelper.CloseLoadingDialog();
                });
            });
            BoxHelper.ShowLoadingDialog();
            if (isInstall == false) { BoxHelper.ShowMessageDialog("Warning", msgOnNotInstall); return false; }
            if (isAndroidO)
            {
                var result = BoxHelper.ShowChoiceDialog("msgNotice", "msgFixAndroidO", "btnDoNotOpen", "btnOpen");
                switch (result)
                {
                    case ChoiceResult.BtnCancel:
                        return false;
                    case ChoiceResult.BtnLeft:
                        fixAndroidO = false;
                        break;
                    case ChoiceResult.BtnRight:
                        fixAndroidO = true;
                        break;
                }
            }
            return true;
        }

        private void ButtonStartBrventService_Click(object sender, RoutedEventArgs e)
        {

            bool fixAndroidO = false;
            var _continue = BreventPrecheck(_currentDevInfo.Serial,
                BreventServiceActivator._AppPackageName,
                "msgPlsInstallBreventFirst",
                ref fixAndroidO);

            if (_continue)
            {
                var args = new ShScriptExecuterArgs() { DevBasicInfo = _currentDevInfo, FixAndroidOAdb = fixAndroidO };
                /*开始操作*/
                BreventServiceActivator activator = new BreventServiceActivator();
                activator.Init(args);
                activator.RunAsync();
                BoxHelper.ShowLoadingDialog(activator);
            }
        }

        private void ButtonPushFileToSdcard_Click(object sender, RoutedEventArgs e)
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
                    DevBasicInfo = _currentDevInfo,
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

        private void ButtonInstallApk_Click(object sender, RoutedEventArgs e)
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
                    DevBasicInfo = _currentDevInfo,
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

        private void ButtonScreentShot_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var shoter = new Basic.Flows.ScreenShoter();
                shoter.Init(new Basic.Flows.ScreenShoterArgs()
                {
                    DevBasicInfo = _currentDevInfo,
                    SavePath = fbd.SelectedPath
                });
                shoter.RunAsync();
                BoxHelper.ShowLoadingDialog(shoter);
            }
        }

        private void ButtonUnlockMiSystem_Click(object sender, RoutedEventArgs e)
        {
            if (!((MainWindow)App.Current.MainWindow).DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!BoxHelper.ShowChoiceDialog("Warning", "warrningNeedRootAccess").ToBool()) return;
            }
            if (BoxHelper.ShowChoiceDialog("msgNotice", "msgUnlockSystemTip") == ChoiceResult.BtnRight)
            {
                var unlocker = new SystemPartitionUnlocker();
                unlocker.Init(new FlowArgs()
                {
                    DevBasicInfo = _currentDevInfo
                });
                unlocker.RunAsync();
                BoxHelper.ShowLoadingDialog(unlocker);
            }
        }

        private void ButtonChangeDpi_Click(object sender, RoutedEventArgs e)
        {
            new DpiChangeWindow(_currentDevInfo) { Owner = App.Current.MainWindow }.ShowDialog();
        }

        private void ButtonExtractBootImg_Click(object sender, RoutedEventArgs e)
        {
            if (!((MainWindow)App.Current.MainWindow).DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!BoxHelper.ShowChoiceDialog("Warning", "warrningNeedRootAccess").ToBool()) return;
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "请选择保存路径"
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;
            var args = new DeviceImageExtractorArgs()
            {
                DevBasicInfo = _currentDevInfo,
                SavePath = fbd.SelectedPath,
                ImageType = DeviceImage.Boot,
            };
            var extrator = new DeviceImageExtractor();
            extrator.Init(args);
            extrator.RunAsync();
            BoxHelper.ShowLoadingDialog(extrator);
        }

        private void ButtonExtractRecImg_Click(object sender, RoutedEventArgs e)
        {
            if (!((MainWindow)App.Current.MainWindow).DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!BoxHelper.ShowChoiceDialog("Warning", "warrningNeedRootAccess").ToBool()) return;
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "请选择保存路径"
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;
            var args = new DeviceImageExtractorArgs()
            {
                DevBasicInfo = _currentDevInfo,
                SavePath = fbd.SelectedPath,
                ImageType = DeviceImage.Recovery,
            };
            var extrator = new DeviceImageExtractor();
            extrator.Init(args);
            extrator.RunAsync();
            BoxHelper.ShowLoadingDialog(extrator);
        }

        private void ButtonFlashBootImg_Click(object sender, RoutedEventArgs e)
        {
            if (!((MainWindow)App.Current.MainWindow).DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!BoxHelper.ShowChoiceDialog("Warning", "warrningNeedRootAccess").ToBool()) return;
            }
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "镜像文件(*.img)|*.img";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var flasherArgs = new DeviceImageFlasherArgs()
                {
                    DevBasicInfo = _currentDevInfo,
                    ImageType = DeviceImage.Boot,
                    SourceFile = fileDialog.FileName,
                };
                DeviceImageFlasher flasher = new DeviceImageFlasher();
                flasher.Init(flasherArgs);
                flasher.RunAsync();
                BoxHelper.ShowLoadingDialog(flasher);
            }
        }

        private async void ButtonDeleteScreenLock_Click(object sender, RoutedEventArgs e)
        {
            if (!((MainWindow)App.Current.MainWindow).DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (BoxHelper.ShowChoiceDialog("Warning", "warrningNeedRootAccess").ToBool() == false) return;
            }
            bool _continue = await Task.Run(() =>
            {
                return BoxHelper.ShowChoiceDialog("Warning", "msgDelScreenLock").ToBool();
            });
            if (!_continue) return;
            var screenLockDeleter = new Basic.Flows.ScreenLockDeleter();
            screenLockDeleter.Init(new FlowArgs()
            {
                DevBasicInfo = _currentDevInfo,
            });
            screenLockDeleter.RunAsync();
            BoxHelper.ShowLoadingDialog(screenLockDeleter);
        }

        private void ButtonFlashRecImg_Click(object sender, RoutedEventArgs e)
        {
            if (!((MainWindow)App.Current.MainWindow).DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!BoxHelper.ShowChoiceDialog("Warning", "warrningNeedRootAccess").ToBool()) return;
            }
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "镜像文件(*.img)|*.img";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var flasherArgs = new DeviceImageFlasherArgs()
                {
                    DevBasicInfo = _currentDevInfo,
                    ImageType = DeviceImage.Recovery,
                    SourceFile = fileDialog.FileName,
                };
                DeviceImageFlasher flasher = new DeviceImageFlasher();
                flasher.Init(flasherArgs);
                flasher.RunAsync();
                BoxHelper.ShowLoadingDialog(flasher);
            }
        }

        private bool DeviceOwnerSetterCheck(string pkgName, string notInstallMsg)
        {
            bool? isInstall = false;
            bool wasRemovedAllUser = false;
            Task.Run(() =>
            {
                isInstall = new DeviceSoftwareInfoGetter(_currentDevInfo.Serial).IsInstall(pkgName);
                wasRemovedAllUser = new UserManager(_currentDevInfo.Serial).GetUsers(true).Length == 0;
                App.Current.Dispatcher.Invoke(() =>
                {
                    BoxHelper.CloseLoadingDialog();
                });
            });
            BoxHelper.ShowLoadingDialog();
            if (isInstall != true) { BoxHelper.ShowMessageDialog("Warning", notInstallMsg); return false; }
            if (!wasRemovedAllUser)
            {
                return
                      BoxHelper.ShowChoiceDialog("Warning",
                      "msgMaybeHaveOtherUser",
                      "btnCancel", "btnIHaveDeletedAllUser").ToBool();
            }
            return true;
        }

        private void ButtonIceBoxAct_Click(object sender, RoutedEventArgs e)
        {
            if (!DeviceOwnerSetterCheck(IceBoxActivator.AppPackageName, "msgPlsInstallIceBoxFirst")) return;

            /*开始操作 */
            IceBoxActivator iceBoxActivator = new IceBoxActivator();
            iceBoxActivator.Init(new FlowArgs() { DevBasicInfo = _currentDevInfo });
            iceBoxActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(iceBoxActivator);
        }

        private void ButtonAirForzenAct_Click(object sender, RoutedEventArgs e)
        {
            if (!DeviceOwnerSetterCheck(AirForzenActivator.AppPackageName, "msgPlsInstallAirForzenFirst")) return;

            /*开始操作*/
            AirForzenActivator airForzenActivator = new AirForzenActivator();
            airForzenActivator.Init(new FlowArgs() { DevBasicInfo = _currentDevInfo });
            airForzenActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(airForzenActivator);
        }

        private void ButtonShizukuManager_Click(object sender, RoutedEventArgs e)
        {
            bool fixAndroidO = false;
            var _continue = BreventPrecheck(_currentDevInfo.Serial,
                ShizukuManagerActivator._AppPackageName,
                "msgPlsInstallShizukuManagerFirst",
                ref fixAndroidO);

            if (_continue)
            {
                var args = new ShScriptExecuterArgs() { DevBasicInfo = _currentDevInfo, FixAndroidOAdb = fixAndroidO };
                /*开始操作*/
                ShizukuManagerActivator activator = new ShizukuManagerActivator();
                activator.Init(args);
                activator.RunAsync();
                BoxHelper.ShowLoadingDialog(activator);
            }
            ///*检查是否安装了这个App*/
            //bool? isInstallThisApp = await Task.Run(() =>
            //{
            //    return new DeviceSoftwareInfoGetter(_currentDevInfo.Serial).IsInstall(ShizukuManagerActivator._AppPackageName);
            //});
            //if (isInstallThisApp == false) { BoxHelper.ShowMessageDialog("Warning", "msgPlsInstallShizukuManagerFirst"); return; }

            ///*判断是否是安卓8.0操作系统*/
            //bool isAndroidO = false;
            //try
            //{
            //    Version currentDevAndroidVersion = new DeviceBuildPropGetter(_currentDevInfo.Serial).GetAndroidVersion();
            //    isAndroidO = currentDevAndroidVersion >= new Version("8.0");
            //}
            //catch (NullReferenceException) { }

            ///*如果是安卓O,询问用户是否要在启动脚本后开启网络ADB*/
            //var args = new ShScriptExecuterArgs() { DevBasicInfo = _currentDevInfo };
            //if (isAndroidO)
            //{
            //    var result = BoxHelper.ShowChoiceDialog("msgNotice",
            //        "msgFixAndroidO",
            //        "btnDoNotOpen", "btnOpen");
            //    switch (result)
            //    {
            //        case ChoiceResult.BtnCancel:
            //            return;
            //        case ChoiceResult.BtnLeft:
            //            args.FixAndroidOAdb = false;
            //            break;
            //        case ChoiceResult.BtnRight:
            //            args.FixAndroidOAdb = true;
            //            break;
            //    }
            //}
            ///*开始操作*/
            //ShizukuManagerActivator activator = new ShizukuManagerActivator();
            //activator.Init(args);
            //activator.RunAsync();
            //BoxHelper.ShowLoadingDialog(activator);
        }

        private void ButtonIslandAct_Click(object sender, RoutedEventArgs e)
        {
            if (!DeviceOwnerSetterCheck(IslandActivator.AppPackageName, "msgPlsInstallIslandFirst")) return;
            /*开始操作*/
            IslandActivator islandActivator = new IslandActivator();
            islandActivator.Init(new FlowArgs() { DevBasicInfo = _currentDevInfo });
            islandActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(islandActivator);
        }

        private void ButtonVirtualBtnHide_Click(object sender, RoutedEventArgs e)
        {
            var choiceResult = BoxHelper.ShowChoiceDialog("PleaseSelected",
                    "msgVirtualButtonHider",
                    "btnHide",
                    "btnUnhide");
            if (choiceResult == ChoiceResult.BtnCancel) return;
            var args = new VirtualButtonHiderArgs()
            {
                DevBasicInfo = _currentDevInfo,
                IsHide = (choiceResult == ChoiceResult.BtnRight),
            };
            VirtualButtonHider hider = new VirtualButtonHider();
            hider.Init(args);
            hider.RunAsync();
            BoxHelper.ShowLoadingDialog(hider);
        }

        private void BtnBackupDcim_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = App.Current.Resources["selectDcimBackupFloder"].ToString()
            };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var args = new DcimBackuperArgs()
                {
                    DevBasicInfo = _currentDevInfo,
                    TargetPath = fbd.SelectedPath + "\\backup"
                };
                var backuper = new DcimBackuper();
                backuper.Init(args);
                new PullingWindow(backuper) { Owner = App.Current.MainWindow }.Show();
            }
        }

        private void ButtonGMCAct_Click(object sender, RoutedEventArgs e)
        {
            var _continue = BoxHelper.ShowChoiceDialog("warning", "msgActiveGMC", "btnCancel", "btnContinue").ToBool();
            if (_continue)
            {
                var activator = new GeekMemoryCleanerActivator();
                activator.Init(new FlowArgs() { DevBasicInfo = this._currentDevInfo });
                activator.RunAsync();
                BoxHelper.ShowLoadingDialog(activator);
            }
        }

        private void ButtonActivateStopapp_Click(object sender, RoutedEventArgs e)
        {
            if (!DeviceOwnerSetterCheck(StopAppActivator.AppPackageName, "msgPlsInstallStopAppFirst")) return;
            /*开始操作*/
            StopAppActivator activator = new StopAppActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = _currentDevInfo });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        private void ButtonUserManager_Click(object sender, RoutedEventArgs e)
        {
            new UserManagerWindow(_currentDevInfo.Serial) { Owner = App.Current.MainWindow }.ShowDialog();
        }

        private void ButtonBlackHole_Click(object sender, RoutedEventArgs e)
        {
            if (!DeviceOwnerSetterCheck(BlackHoleActivator.AppPackageName, "msgPlsInstallBlackholeFirst")) return;
            BlackHoleActivator activator = new BlackHoleActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = _currentDevInfo });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        private void ButtonAnzenbokusuActivator_Click(object sender, RoutedEventArgs e)
        {
            if (!DeviceOwnerSetterCheck(AnzenbokusuActivator.AppPackageName, "msgPlsInstallAnzenbokusuFirst")) return;
            AnzenbokusuActivator activator = new AnzenbokusuActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = _currentDevInfo });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }
    }
}
