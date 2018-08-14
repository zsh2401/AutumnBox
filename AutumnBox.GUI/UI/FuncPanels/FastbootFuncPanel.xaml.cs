using AutumnBox.Basic.Device;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.OpenFramework.Warpper;
using System.Linq;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// FastbootFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class FastbootFuncPanel : FastPanelChild, IExtPanel
    {
        //private DeviceBasicInfo _currentDeviceInfo;
        public FastbootFuncPanel()
        {
            InitializeComponent();
            //ExtPanel.TargetDeviceState = new DeviceState[] {
            //    DeviceState.Fastboot
            //};
        }

        //public void Refresh(DeviceBasicInfo devInfo)
        //{
        //    _currentDeviceInfo = devInfo;
        //    UIHelper.SetGridButtonStatus(MainGrid, _currentDeviceInfo.State == DeviceState.Fastboot);
        //}

        //public void Reset()
        //{
        //    UIHelper.SetGridButtonStatus(MainGrid, false);
        //}

        //private void ButtonFlashCustomRecovery_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog fileDialog = new OpenFileDialog();
        //    fileDialog.Reset();
        //    fileDialog.Title = "选择一个文件";
        //    fileDialog.Filter = "镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
        //    fileDialog.Multiselect = false;
        //    if (fileDialog.ShowDialog() == true)
        //    {
        //        var flasher = new RecoveryFlasher();
        //        flasher.Init(new RecoveryFlasherArgs()
        //        {
        //            DevBasicInfo = _currentDeviceInfo,
        //            RecoveryFilePath = fileDialog.FileName,
        //        });
        //        flasher.RunAsync();
        //        BoxHelper.ShowLoadingDialog(flasher);
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        //private void ButtonMiFlash_Click(object sender, RoutedEventArgs e)
        //{
        //    new MiFlashWindow().Show();
        //}

        //private void ButtonRelockMi_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!BoxHelper.ShowChoiceDialog("Warning", "msgRelockWarning").ToBool()) return;
        //    if (!BoxHelper.ShowChoiceDialog("Warning", "msgRelockWarningAgain").ToBool()) return;
        //    var oemRelocker = new OemRelocker();
        //    oemRelocker.Init(new Basic.FlowFramework.FlowArgs()
        //    {
        //        DevBasicInfo = _currentDeviceInfo
        //    });
        //    oemRelocker.RunAsync();
        //    BoxHelper.ShowLoadingDialog(oemRelocker);
        //}

        //private void BtnMiFlash_Click(object sender, RoutedEventArgs e)
        //{
        //    new MiFlashWindow()
        //    {
        //        ShowInfo = new MiFlashWindowShowInfo()
        //        {
        //            Owner = App.Current.MainWindow,
        //            Serial = _currentDeviceInfo.Serial,
        //        }
        //    }.Show();
        //}

        public void Set(IExtensionWarpper[] warppers, DeviceBasicInfo currentDevice)
        {
            ExtPanel.Set(warppers, currentDevice);
        }

        public void Set(IExtensionWarpper[] warppers)
        {
            ExtPanel.Set(warppers);
        }
    }
}
