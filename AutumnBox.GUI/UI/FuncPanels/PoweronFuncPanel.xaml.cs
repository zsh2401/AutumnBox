using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using System;
using System.Windows;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Function;
using System.Windows.Forms;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using System.Threading.Tasks;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows;
using System.IO;
using System.Collections.Generic;
using AutumnBox.Support.CstmDebug;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// PoweronFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class PoweronFuncPanel : System.Windows.Controls.UserControl, IRefreshable
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

        private async void ButtonStartBrventService_Click(object sender, RoutedEventArgs e)
        {
            /*检查是否安装了这个App*/
            bool? isInstallThisApp = await Task.Run(() =>
            {
                return DeviceInfoHelper.IsInstalled(_currentDevInfo.Serial, BreventServiceActivator.AppPackageName);
            });
            if (isInstallThisApp == false) { BoxHelper.ShowMessageDialog("Warning", "msgPlsInstallBreventFirst"); return; }
            /*判断是否是安卓8.0操作系统*/
            bool isAndroidO = false;
            try
            {
                Version currentDevAndroidVersion = ((MainWindow)System.Windows.Application.Current.MainWindow).DevInfoPanel.CurrentDeviceAndroidVersion;
                isAndroidO = currentDevAndroidVersion >= new Version("8.0");
            }
            catch (NullReferenceException) { }
            /*如果是安卓O,询问用户是否要在启动脚本后开启网络ADB*/
            var args = new BreventServiceActivatorArgs() { DevBasicInfo = _currentDevInfo };
            if (isAndroidO)
            {
                var result = BoxHelper.ShowChoiceDialog("msgNotice", "msgBreventFixTip", "btnDoNotOpen", "btnOpen");
                switch (result)
                {
                    case Windows.ChoiceResult.BtnCancel:
                        return;
                    case Windows.ChoiceResult.BtnLeft:
                        args.FixAndroidOAdb = false;
                        break;
                    case Windows.ChoiceResult.BtnRight:
                        args.FixAndroidOAdb = true;
                        break;
                }
            }
            /*开始操作*/
            BreventServiceActivator activator = new BreventServiceActivator();
            activator.Init(args);
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
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
                var fmp = FunctionModuleProxy.Create<Basic.Function.Modules.FileSender>(new FileSenderArgs(_currentDevInfo) { FilePath = fileDialog.FileName });
                fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
                fmp.AsyncRun();
                new FileSendingWindow(fmp).ShowDialog();
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
                Basic.Flows.ApkInstaller installer = new Basic.Flows.ApkInstaller();
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
                var fmp = FunctionModuleProxy.Create<ScreenShoter>(new ScreenShoterArgs(_currentDevInfo) { LocalPath = fbd.SelectedPath });
                fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
                fmp.AsyncRun();
                BoxHelper.ShowLoadingDialog(fmp);
            }
            else
            {
                return;
            }
        }

        private void ButtonUnlockMiSystem_Click(object sender, RoutedEventArgs e)
        {

            if (BoxHelper.ShowChoiceDialog("msgNotice", "msgUnlockSystemTip") == ChoiceResult.BtnRight)
            {
                var fmp = FunctionModuleProxy.Create<SystemUnlocker>(new ModuleArgs(_currentDevInfo));
                fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
                fmp.AsyncRun();
                BoxHelper.ShowLoadingDialog(fmp);
            }
        }

        private void ButtonChangeDpi_Click(object sender, RoutedEventArgs e)
        {
            new DpiChangeWindow(_currentDevInfo) { Owner = App.Current.MainWindow }.ShowDialog();
        }

        private void ButtonFullBackup_Click(object sender, RoutedEventArgs ex)
        {
            //BlockHelper.ShowMessageBlock("msgNotice", "msgNoticeForAndroidFullBackupRemove");
            //bool _screenIsOpen = ChoiceBox.FastShow(App.OwnerWindow,
            //    UIHelper.GetString("msgNotice"), 
            //    UIHelper.GetString("msgOpenTheScreenPls"),
            //    UIHelper.GetString("btnContinue"),
            //    UIHelper.GetString("btnCancel")) ;
            //if (!_screenIsOpen) return;
            //var fmp = FunctionModuleProxy.Create(typeof(AndroidFullBackup), new ModuleArgs(App.SelectedDevice));
            //fmp.Finished += App.OwnerWindow.FuncFinish;
            //fmp.Finished += (s, e) => { Logger.D($"Full backup was launched?.... there is output : {e.OutputData.All}"); };
            //fmp.AsyncRun();
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
            FunctionModuleProxy fmp =
                FunctionModuleProxy.Create<ImageExtractor>(new ImgExtractArgs(_currentDevInfo) { ExtractImage = Images.Boot, SavePath = fbd.SelectedPath });
            fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
            fmp.AsyncRun();
            BoxHelper.ShowLoadingDialog(fmp);
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
            FunctionModuleProxy fmp =
                FunctionModuleProxy.Create<ImageExtractor>(new ImgExtractArgs(_currentDevInfo) { ExtractImage = Images.Recovery, SavePath = fbd.SelectedPath });
            fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
            fmp.AsyncRun();
            BoxHelper.ShowLoadingDialog(fmp);
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
                var fmp = FunctionModuleProxy.Create<ImageFlasher>(new ImgFlasherArgs(_currentDevInfo) { ImgPath = fileDialog.FileName, ImgType = Images.Boot });
                fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
                fmp.AsyncRun();
                BoxHelper.ShowLoadingDialog(fmp);
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
            FunctionModuleProxy fmp = FunctionModuleProxy.Create<ScreenLockDeleter>(new ModuleArgs(_currentDevInfo));
            fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
            fmp.AsyncRun();
            BoxHelper.ShowLoadingDialog(fmp);
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
                var fmp = FunctionModuleProxy.Create<ImageFlasher>(new ImgFlasherArgs(_currentDevInfo) { ImgPath = fileDialog.FileName, ImgType = Images.Recovery });
                fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
                fmp.AsyncRun();
                BoxHelper.ShowLoadingDialog(fmp);
            }
        }

        private async void ButtonIceBoxAct_Click(object sender, RoutedEventArgs e)
        {
            /*检查是否安装了这个App*/
            bool? isInstallThisApp = await Task.Run(() =>
            {
                return DeviceInfoHelper.IsInstalled(_currentDevInfo.Serial, IceBoxActivator.AppPackageName);
            });
            if (isInstallThisApp == false) { BoxHelper.ShowMessageDialog("Warning", "msgPlsInstallIceBoxFirst"); return; }
            /*提示用户删除账户*/
            bool _continue = BoxHelper.ShowChoiceDialog("msgNotice",
                    $"{UIHelper.GetString("msgIceActLine1")}\n{UIHelper.GetString("msgIceActLine2")}\n{UIHelper.GetString("msgIceActLine3")}",
                    "btnCancel",
                    "btnContinue").ToBool();
            Logger.D(_continue.ToString());
            if (!_continue) return;
            /*开始操作 */
            IceBoxActivator iceBoxActivator = new IceBoxActivator();
            iceBoxActivator.Init(new FlowArgs() { DevBasicInfo = _currentDevInfo });
            iceBoxActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(iceBoxActivator);
        }

        private async void ButtonAirForzenAct_Click(object sender, RoutedEventArgs e)
        {
            /*检查是否安装了这个App*/
            bool? isInstallThisApp = await Task.Run(() =>
            {
                return DeviceInfoHelper.IsInstalled(_currentDevInfo.Serial, AirForzenActivator.AppPackageName);
            });

            if (isInstallThisApp == false) { BoxHelper.ShowMessageDialog("Warning", "msgPlsInstallAirForzenFirst"); return; }
            /*提示用户删除账户*/
            bool _continue = await Task.Run(() =>
            {
                return BoxHelper.ShowChoiceDialog(
                    "msgNotice",
                    $"{UIHelper.GetString("msgIceActLine1")}\n{UIHelper.GetString("msgIceActLine2")}\n{UIHelper.GetString("msgIceActLine3")}",
                    "btnCancel",
                    "btnContinue").ToBool();
            });
            if (!_continue) return;
            /*开始操作*/
            AirForzenActivator airForzenActivator = new AirForzenActivator();
            airForzenActivator.Init(new FlowArgs() { DevBasicInfo = _currentDevInfo });
            airForzenActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(airForzenActivator);
        }

        private async void ButtonShizukuManager_Click(object sender, RoutedEventArgs e)
        {
            /*检查是否安装了这个App*/
            bool? isInstallThisApp = await Task.Run(() =>
            {
                return DeviceInfoHelper.IsInstalled(_currentDevInfo.Serial, ShizukuManagerActivator.AppPackageName);
            });
            if (isInstallThisApp == false) { BoxHelper.ShowMessageDialog("Warning", "msgPlsInstallShizukuManagerFirst"); return; }
            /*开始操作*/
            ShizukuManagerActivator shizukuManagerActivator = new ShizukuManagerActivator();
            shizukuManagerActivator.Init(new FlowArgs() { DevBasicInfo = _currentDevInfo });
            shizukuManagerActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(shizukuManagerActivator);
        }

        private async void ButtonIslandAct_Click(object sender, RoutedEventArgs e)
        {
            /*检查是否安装了这个App*/
            bool? isInstallThisApp = await Task.Run(() =>
            {
                return DeviceInfoHelper.IsInstalled(_currentDevInfo.Serial, IslandActivator.AppPackageName);
            });
            if (isInstallThisApp == false) { BoxHelper.ShowMessageDialog("Warning", "msgPlsInstallIslandFirst"); return; }
            /*提示用户删除账户*/
            bool _continue = await Task.Run(() =>
            {
                return BoxHelper.ShowChoiceDialog("msgNotice",
                    $"{UIHelper.GetString("msgIceActLine1")}\n{UIHelper.GetString("msgIceActLine2")}\n{UIHelper.GetString("msgIceActLine3")}",
                    "btnCancel",
                    "btnContinue"
                    ).ToBool();
            });
            if (!_continue) return;
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
    }
}
