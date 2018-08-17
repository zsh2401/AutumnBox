using System.Windows;
using System.Windows.Controls;
using AutumnBox.GUI.Helper;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Util.UI;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// RebootButtonsGrid.xaml 的交互逻辑
    /// </summary>
    public partial class RebootBtnPanel : UserControl, IDeviceRefreshable
    {
        private DeviceBasicInfo _currentDevInfo;
        public RebootBtnPanel()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            _currentDevInfo = new DeviceBasicInfo() { State = DeviceState.None };
            ButtonRebootToBootloader.IsEnabled = false;
            ButtonRebootToRecovery.IsEnabled = false;
            ButtonRebootToSystem.IsEnabled = false;
            ButtonRebootToSnapdragon9008.IsEnabled = false;
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            _currentDevInfo = deviceSimpleInfo;
            this.Dispatcher.Invoke(() =>
            {
                switch (deviceSimpleInfo.State)
                {
                    case DeviceState.Fastboot:
                        ButtonRebootToBootloader.IsEnabled = true;
                        ButtonRebootToRecovery.IsEnabled = false;
                        ButtonRebootToSystem.IsEnabled = true;
                        ButtonRebootToSnapdragon9008.IsEnabled = true;
                        break;
                    case DeviceState.Recovery:
                    case DeviceState.Poweron:
                        ButtonRebootToBootloader.IsEnabled = true;
                        ButtonRebootToRecovery.IsEnabled = true;
                        ButtonRebootToSystem.IsEnabled = true;
                        ButtonRebootToSnapdragon9008.IsEnabled = true;
                        break;
                    default:
                        ButtonRebootToBootloader.IsEnabled = false;
                        ButtonRebootToRecovery.IsEnabled = false;
                        ButtonRebootToSystem.IsEnabled = false;
                        ButtonRebootToSnapdragon9008.IsEnabled = false;
                        break;
                }
            });
        }

        private void ButtonRebootToSystem_Click(object sender, RoutedEventArgs e)
        {
            DeviceRebooter.RebootAsync(_currentDevInfo, RebootOptions.System);
        }

        private void ButtonRebootToRecovery_Click(object sender, RoutedEventArgs e)
        {
            DeviceRebooter.RebootAsync(_currentDevInfo, RebootOptions.Recovery);
        }

        private void ButtonRebootToBootloader_Click(object sender, RoutedEventArgs e)
        {
            DeviceRebooter.RebootAsync(_currentDevInfo, RebootOptions.Fastboot);
        }

        private void ButtonRebootToSnapdragon9008_Click(object sender, RoutedEventArgs e)
        {
            bool _needToContinue = BoxHelper.ShowChoiceDialog("msgNotice",
                                    UIHelper.GetString("msgNoticeForRebootToEdlLine1") + "\n" +
                                    UIHelper.GetString("msgNoticeForRebootToEdlLine2") + "\n" +
                                     UIHelper.GetString("msgNoticeForRebootToEdlLine3"),
                                    "btnCancel",
                                    "btnContinue").ToBool();
            if (!_needToContinue) return;
            DeviceRebooter.RebootAsync(_currentDevInfo, RebootOptions.Snapdragon9008);
        }
    }
}
