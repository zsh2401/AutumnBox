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
using AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// PoweronFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class PoweronFuncPanel : FastPanelChild, IRefreshable
    {
        private DeviceBasicInfo _currentDevInfo;
        private IPoweronFuncsUX ux;
        public PoweronFuncPanel()
        {
            InitializeComponent();
            ux = App.MainAopContext.GetObject<IPoweronFuncsUX>("poweronFuncsUXImp");
        }

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
            ux.ActivateBrevent(_currentDevInfo);
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
                var shoter = new ScreenShoter();
                shoter.Init(new ScreenShoterArgs()
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
            ux.ActivateIceBox(_currentDevInfo);
        }

        private void ButtonAirForzenAct_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateAirForzen(_currentDevInfo);
        }

        private void ButtonShizukuManager_Click(object sender, RoutedEventArgs e)
        {

            ux.ActivateShizukuManager(_currentDevInfo);
        }

        private void ButtonIslandAct_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateIsland(_currentDevInfo);
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
            ux.ActivateGeekMemoryCleaner(_currentDevInfo);
        }

        private void ButtonActivateStopapp_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateStopapp(_currentDevInfo);
        }

        private void ButtonUserManager_Click(object sender, RoutedEventArgs e)
        {
            new UserManagerWindow(_currentDevInfo.Serial) { Owner = App.Current.MainWindow }.ShowDialog();
        }

        private void ButtonBlackHole_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateBlackHole(_currentDevInfo);
        }

        private void ButtonAnzenbokusuActivator_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateAnzenbokusu(_currentDevInfo);
        }

        private void ButtonActivateFreezeYou_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateFreezeYou(_currentDevInfo);
        }

        private void ButtonAnzenbokusuFakeActivator_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateAnzenbokusuFake(_currentDevInfo);
        }

        private void ButtonActivateGreenifyDoze_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateGreenifyAggressiveDoze(_currentDevInfo);
        }

        private void ButtonUsersirAct_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateUsersir(_currentDevInfo);
        }
    }
}
