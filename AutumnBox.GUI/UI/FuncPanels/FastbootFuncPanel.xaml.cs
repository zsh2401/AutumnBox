using Microsoft.Win32;
using System.Windows;
using AutumnBox.GUI.Helper;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Windows;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.Basic.Flows;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// FastbootFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class FastbootFuncPanel : FastPanelChild, IRefreshable
    {
        private DeviceBasicInfo _currentDeviceInfo;
        public FastbootFuncPanel()
        {
            InitializeComponent();
        }

        public void Refresh(DeviceBasicInfo devInfo)
        {
            _currentDeviceInfo = devInfo;
            UIHelper.SetGridButtonStatus(MainGrid, _currentDeviceInfo.State == DeviceState.Fastboot);
        }

        public void Reset()
        {
            UIHelper.SetGridButtonStatus(MainGrid, false);
        }

        private void ButtonFlashCustomRecovery_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = "选择一个文件";
            fileDialog.Filter = "镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var flasher = new RecoveryFlasher();
                flasher.Init(new RecoveryFlasherArgs() {
                    DevBasicInfo = _currentDeviceInfo,
                    RecoveryFilePath = fileDialog.FileName,
                });
                flasher.RunAsync();
                BoxHelper.ShowLoadingDialog(flasher);
            }
            else
            {
                return;
            }
        }

        private void ButtonMiFlash_Click(object sender, RoutedEventArgs e)
        {
            new MiFlashWindow().Show();
        }

        private void ButtonRelockMi_Click(object sender, RoutedEventArgs e)
        {
            if (!BoxHelper.ShowChoiceDialog("Warning", "msgRelockWarning").ToBool()) return;
            if (!BoxHelper.ShowChoiceDialog("Warning", "msgRelockWarningAgain").ToBool()) return;
            var oemRelocker = new OemRelocker();
            oemRelocker.Init(new Basic.FlowFramework.FlowArgs() {
                DevBasicInfo = _currentDeviceInfo
            });
            oemRelocker.RunAsync();
            BoxHelper.ShowLoadingDialog(oemRelocker);
        }

        private void BtnMiFlash_Click(object sender, RoutedEventArgs e)
        {
            new MiFlashWindow()
            {
                ShowInfo = new MiFlashWindowShowInfo()
                {
                    Owner = App.Current.MainWindow,
                    Serial = _currentDeviceInfo.Serial,
                }
            }.Show();
        }
    }
}
