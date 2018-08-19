using System.Windows;
using AutumnBox.GUI.Windows;
using AutumnBox.Basic.Flows;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx;
using AutumnBox.OpenFramework.Warpper;
using System.Linq;
using AutumnBox.GUI.Util.UI;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// PoweronFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class PoweronFuncPanel : FastPanelChild, IExtPanel
    {
        public void Set(IExtensionWarpper[] warppers, DeviceBasicInfo currentDevice)
        {
            var filted = from warpper in warppers
                         where warpper.Info.RequiredDeviceStates.HasFlag(DeviceState.Poweron)
                         select warpper;
            ExtPanel.Set(filted.ToArray(), currentDevice);
        }
        private DeviceBasicInfo _currentDevInfo;
        private IPoweronFuncsUX ux;

        public PoweronFuncPanel()
        {
            InitializeComponent();
            //ExtPanel.TargetDeviceState = new DeviceState[] {
            //    DeviceState.Poweron
            //};
            //ux = App.Current.SpringContext.GetObject<IPoweronFuncsUX>("poweronFuncsUXImp");
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

        private void ButtonStartBrventService_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateBrevent(_currentDevInfo);
        }

        private void ButtonPushFileToSdcard_Click(object sender, RoutedEventArgs e)
        {
            ux.PushFile(_currentDevInfo);
        }

        private void ButtonInstallApk_Click(object sender, RoutedEventArgs e)
        {
            ux.InstallApk(_currentDevInfo);
        }

        private void ButtonScreentShot_Click(object sender, RoutedEventArgs e)
        {
            ux.ScreenShot(_currentDevInfo);
        }

        private void ButtonUnlockMiSystem_Click(object sender, RoutedEventArgs e)
        {
            ux.UnlockSystemParation(_currentDevInfo);
        }

        private void ButtonChangeDpi_Click(object sender, RoutedEventArgs e)
        {
            ux.ChangeDpi(_currentDevInfo);
        }

        private void ButtonExtractBootImg_Click(object sender, RoutedEventArgs e)
        {
            ux.ExtractBoot(_currentDevInfo);
        }

        private void ButtonExtractRecImg_Click(object sender, RoutedEventArgs e)
        {
            ux.ExtractRecovery(_currentDevInfo);
        }

        private void ButtonFlashBootImg_Click(object sender, RoutedEventArgs e)
        {
            ux.FlashBoot(_currentDevInfo);
        }

        private void ButtonDeleteScreenLock_Click(object sender, RoutedEventArgs e)
        {
            ux.DeleteScreenLock(_currentDevInfo);
        }

        private void ButtonFlashRecImg_Click(object sender, RoutedEventArgs e)
        {
            ux.FlashRecovery(_currentDevInfo);
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
            ux.BackupDcim(_currentDevInfo);
            //FolderBrowserDialog fbd = new FolderBrowserDialog
            //{
            //    Description = App.Current.Resources["selectDcimBackupFloder"].ToString()
            //};
            //if (fbd.ShowDialog() == DialogResult.OK)
            //{
            //    var args = new DcimBackuperArgs()
            //    {
            //        DevBasicInfo = _currentDevInfo,
            //        TargetPath = Path.Combine(fbd.SelectedPath)
            //    };
            //    var backuper = new DcimBackuper();
            //    backuper.Init(args);
            //    new PullingWindow(backuper) { Owner = App.Current.MainWindow }.Show();
            //}
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

        private void ButtonActAppOpsX_Click(object sender, RoutedEventArgs e)
        {
            ux.ActivateAppOpsX(_currentDevInfo);
        }

        public void Set(IExtensionWarpper[] warppers)
        {
            var filted = from warpper in warppers
                         where warpper.Info.RequiredDeviceStates.HasFlag(DeviceState.Poweron)
                         select warpper;
            ExtPanel.Set(filted.ToArray());
        }
    }
}
