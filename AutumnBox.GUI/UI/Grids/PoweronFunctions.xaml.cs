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
            if (!ChoiceBox.FastShow(App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgStartBrventTip"].ToString())) return;
            var fmp = FunctionModuleProxy.Create<BreventServiceActivator>(new ModuleArgs(App.SelectedDevice));
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
            UIHelper.ShowRateBox(fmp);
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
                var fmp = FunctionModuleProxy.Create<FileSender>(new FileArgs(App.SelectedDevice) { files = new string[] { fileDialog.FileName } });
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
            //MMessageBox.ShowDialog(FindResource("Notice").ToString(), DeviceInfoHelper.GetDpi(App.SelectedDevice).ToString());
        }

        private void ButtonFullBackup_Click(object sender, RoutedEventArgs ex)
        {
            var fmp = FunctionModuleProxy.Create(typeof(AndroidFullBackup), new ModuleArgs(App.SelectedDevice));
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.Finished += (s, e) => { Logger.D($"Full backup was launched?.... there is output : {e.OutputData.All}"); };
            fmp.AsyncRun();
            new FastBrowser().ShowDialog();
        }

        private void ButtonExtractBootImg_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.FastShow(App.OwnerWindow, App.Current.Resources["Warning"].ToString(), App.Current.Resources["warrningNeedRootAccess"].ToString())) return;
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "请选择保存路径"
            };
            if (fbd.ShowDialog() != DialogResult.OK) return;
            FunctionModuleProxy fmp =
                FunctionModuleProxy.Create<ImageExtractor>(new ImgExtractArgs(App.SelectedDevice) { ExtractImage = Basic.Function.Args.Image.Boot, SavePath = fbd.SelectedPath });
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
            UIHelper.ShowRateBox(fmp);
        }

        private void ButtonExtractRecImg_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.FastShow(App.OwnerWindow, App.Current.Resources["Warning"].ToString(), App.Current.Resources["warrningNeedRootAccess"].ToString())) return;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "请选择保存路径";
            if (fbd.ShowDialog() != DialogResult.OK) return;
            FunctionModuleProxy fmp =
                FunctionModuleProxy.Create<ImageExtractor>(new ImgExtractArgs(App.SelectedDevice) { ExtractImage = Basic.Function.Args.Image.Recovery, SavePath = fbd.SelectedPath });
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
            UIHelper.ShowRateBox(fmp);
        }

        private void ButtonFlashBootImg_Click(object sender, RoutedEventArgs e)
        {
            MMessageBox.FastShow(App.OwnerWindow, App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgFunctionIsInTheDeveloping"].ToString());
        }

        private void ButtonDeleteScreenLock_Click(object sender, RoutedEventArgs e)
        {
            MMessageBox.FastShow(App.OwnerWindow, App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgFunctionIsInTheDeveloping"].ToString());
        }

        private void ButtonFullBackup_Copy_Click(object sender, RoutedEventArgs e)
        {
            MMessageBox.FastShow(App.OwnerWindow, App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgFunctionIsInTheDeveloping"].ToString());
        }

        private void ButtonFlashRecImg_Click(object sender, RoutedEventArgs e)
        {
            MMessageBox.FastShow(App.OwnerWindow, App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgFunctionIsInTheDeveloping"].ToString());
        }
    }
}
