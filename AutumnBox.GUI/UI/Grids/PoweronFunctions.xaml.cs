using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using System;
using System.Windows;
using System.Windows.Controls;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Function;
using System.Windows.Forms;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.CstmDebug;
using System.Threading.Tasks;
using AutumnBox.Basic.FlowFramework;

namespace AutumnBox.GUI.UI.Grids
{
    /// <summary>
    /// PoweronFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class PoweronFunctions : Grid, IDeviceInfoRefreshable
    {
        public PoweronFunctions()
        {
            InitializeComponent();
        }

        public event EventHandler RefreshStart;
        public event EventHandler RefreshFinished;
        public void SetDefault()
        {
            UIHelper.SetGridButtonStatus(this, false);
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            RefreshStart?.Invoke(this, new EventArgs());
            bool status = deviceSimpleInfo.Status == DeviceStatus.Poweron;
            UIHelper.SetGridButtonStatus(this, status);
            RefreshFinished?.Invoke(this, new EventArgs());
        }

        private void ButtonStartBrventService_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.FastShow(App.OwnerWindow, App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgStartBrventTip"].ToString())) return;
            Basic.Flows.BreventServiceActivator bsa = new Basic.Flows.BreventServiceActivator();
            bsa.Init(new Basic.FlowFramework.Args.FlowArgs() { DevBasicInfo = App.SelectedDevice });
            bsa.RunAsync();
            UIHelper.ShowRateBox(bsa);
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
                var fmp = FunctionModuleProxy.Create<FileSender>(new FileSenderArgs(App.SelectedDevice) { FilePath = fileDialog.FileName });
                fmp.Finished += App.OwnerWindow.FuncFinish;
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
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var fmp = FunctionModuleProxy.Create<ApkInstaller>(new InstallApkArgs(App.SelectedDevice) { ApkPath = fileDialog.FileName });
                fmp.Finished += App.OwnerWindow.FuncFinish;
                fmp.AsyncRun();
                UIHelper.ShowRateBox(fmp);
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
                var fmp = FunctionModuleProxy.Create<ScreenShoter>(new ScreenShoterArgs(App.SelectedDevice) { LocalPath = fbd.SelectedPath });
                fmp.Finished += App.OwnerWindow.FuncFinish;
                fmp.AsyncRun();
                UIHelper.ShowRateBox(fmp);
            }
            else
            {
                return;
            }
        }

        private void ButtonUnlockMiSystem_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.FastShow(FindResource("Notice").ToString(), FindResource("msgUnlockXiaomiSystemTip").ToString())) return;
            MMessageBox.FastShow(App.OwnerWindow, FindResource("Notice").ToString(), FindResource("msgIfAllOK").ToString());
            var fmp = FunctionModuleProxy.Create<XiaomiBootloaderRelocker>(new ModuleArgs(App.SelectedDevice));
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
            UIHelper.ShowRateBox(fmp);
        }


        private void ButtonChangeDpi_Click(object sender, RoutedEventArgs e)
        {
            DpiChangeWindow.FastShow(App.OwnerWindow);
            //MMessageBox.ShowDialog(FindResource("Notice").ToString(), DeviceInfoHelper.GetDpi(App.SelectedDevice).ToString());
        }

        private void ButtonFullBackup_Click(object sender, RoutedEventArgs ex)
        {
            MMessageBox.FastShow(App.OwnerWindow, UIHelper.GetString("msgNotice"), UIHelper.GetString("msgNoticeForAndroidFullBackupRemove"));
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
            if (!App.OwnerWindow.DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!ChoiceBox.FastShow(App.OwnerWindow, App.Current.Resources["Warning"].ToString(), App.Current.Resources["warrningNeedRootAccess"].ToString())) return;
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "请选择保存路径"
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;
            FunctionModuleProxy fmp =
                FunctionModuleProxy.Create<ImageExtractor>(new ImgExtractArgs(App.SelectedDevice) { ExtractImage = Images.Boot, SavePath = fbd.SelectedPath });
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
            UIHelper.ShowRateBox(fmp);
        }

        private void ButtonExtractRecImg_Click(object sender, RoutedEventArgs e)
        {
            if (!App.OwnerWindow.DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!ChoiceBox.FastShow(App.OwnerWindow, App.Current.Resources["Warning"].ToString(), App.Current.Resources["warrningNeedRootAccess"].ToString())) return;
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "请选择保存路径"
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;
            FunctionModuleProxy fmp =
                FunctionModuleProxy.Create<ImageExtractor>(new ImgExtractArgs(App.SelectedDevice) { ExtractImage = Images.Recovery, SavePath = fbd.SelectedPath });
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
            UIHelper.ShowRateBox(fmp);
        }

        private void ButtonFlashBootImg_Click(object sender, RoutedEventArgs e)
        {
            if (!App.OwnerWindow.DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!ChoiceBox.FastShow(App.OwnerWindow, App.Current.Resources["Warning"].ToString(), App.Current.Resources["warrningNeedRootAccess"].ToString())) return;
            }
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "镜像文件(*.img)|*.img";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var fmp = FunctionModuleProxy.Create<ImageFlasher>(new ImgFlasherArgs(App.SelectedDevice) { ImgPath = fileDialog.FileName, ImgType = Images.Boot });
                fmp.Finished += App.OwnerWindow.FuncFinish;
                fmp.AsyncRun();
                UIHelper.ShowRateBox(fmp);
            }
        }

        private void ButtonDeleteScreenLock_Click(object sender, RoutedEventArgs e)
        {
            if (!App.OwnerWindow.DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!ChoiceBox.FastShow(App.OwnerWindow, App.Current.Resources["Warning"].ToString(), App.Current.Resources["warrningNeedRootAccess"].ToString())) return;
            }
            if (!ChoiceBox.FastShow(App.OwnerWindow, App.Current.Resources["Warning"].ToString(), App.Current.Resources["msgDelScreenLock"].ToString())) return;
            FunctionModuleProxy fmp = FunctionModuleProxy.Create<ScreenLockDeleter>(new ModuleArgs(App.SelectedDevice));
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
            UIHelper.ShowRateBox();
        }

        private void ButtonFullBackup_Copy_Click(object sender, RoutedEventArgs e)
        {
            MMessageBox.FastShow(App.OwnerWindow, App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgFunctionIsInTheDeveloping"].ToString());
        }

        private void ButtonFlashRecImg_Click(object sender, RoutedEventArgs e)
        {
            if (!App.OwnerWindow.DevInfoPanel.CurrentDeviceIsRoot)
            {
                if (!ChoiceBox.FastShow(App.OwnerWindow, App.Current.Resources["Warning"].ToString(), App.Current.Resources["warrningNeedRootAccess"].ToString())) return;
            }
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "镜像文件(*.img)|*.img";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var fmp = FunctionModuleProxy.Create<ImageFlasher>(new ImgFlasherArgs(App.SelectedDevice) { ImgPath = fileDialog.FileName, ImgType = Images.Recovery });
                fmp.Finished += App.OwnerWindow.FuncFinish;
                fmp.AsyncRun();
                UIHelper.ShowRateBox(fmp);
            }
        }

        private void ButtonIceBoxAct_Click(object sender, RoutedEventArgs e)
        {
            bool _continue = ChoiceBox.FastShow(App.OwnerWindow,
                UIHelper.GetString("msgNotice"),
                $"{UIHelper.GetString("msgIceActLine1")}\n{UIHelper.GetString("msgIceActLine2")}\n{UIHelper.GetString("msgIceActLine3")}", UIHelper.GetString("btnContinue"), UIHelper.GetString("btnCancel"));
            if (!_continue) return;
            Basic.Flows.IceBoxActivator iceBoxActivator = new Basic.Flows.IceBoxActivator();
            iceBoxActivator.Init(new Basic.FlowFramework.Args.FlowArgs() { DevBasicInfo = App.SelectedDevice });
            iceBoxActivator.RunAsync();
            UIHelper.ShowRateBox(iceBoxActivator);
        }

        private void ButtonAirForzenAct_Click(object sender, RoutedEventArgs e)
        {
            bool _continue = ChoiceBox.FastShow(App.OwnerWindow,
                 UIHelper.GetString("msgNotice"),
                $"{UIHelper.GetString("msgIceActLine1")}\n{UIHelper.GetString("msgIceActLine2")}\n{UIHelper.GetString("msgIceActLine3")}", UIHelper.GetString("btnContinue"), UIHelper.GetString("btnCancel"));
            if (!_continue) return;
            Basic.Flows.AirForzenActivator airForzenActivator = new Basic.Flows.AirForzenActivator();
            airForzenActivator.Init(new Basic.FlowFramework.Args.FlowArgs() { DevBasicInfo = App.SelectedDevice });
            airForzenActivator.RunAsync();
            UIHelper.ShowRateBox(airForzenActivator);
        }

        private void ButtonShizukuManager_Click(object sender, RoutedEventArgs e)
        {
            Basic.Flows.ShizukuManagerActivator shizukuManagerActivator = new Basic.Flows.ShizukuManagerActivator();
            shizukuManagerActivator.Init(new Basic.FlowFramework.Args.FlowArgs() { DevBasicInfo = App.SelectedDevice });
            shizukuManagerActivator.RunAsync();
            UIHelper.ShowRateBox(shizukuManagerActivator);
        }
    }
}
