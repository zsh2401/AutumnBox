using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutumnBox.Basic.Devices;
using AutumnBox.GUI.Windows;
using AutumnBox.GUI.Helper;

namespace AutumnBox.GUI.UI.Grids
{
    /// <summary>
    /// RebootButtonsGrid.xaml 的交互逻辑
    /// </summary>
    public partial class RebootButtonsGrid : UserControl, IDeviceInfoRefreshable
    {
        public RebootButtonsGrid()
        {
            InitializeComponent();
        }

        public event EventHandler RefreshStart;
        public event EventHandler RefreshFinished;

        public void SetDefault()
        {
            ButtonRebootToBootloader.IsEnabled = false;
            ButtonRebootToRecovery.IsEnabled = false;
            ButtonRebootToSystem.IsEnabled = false;
            ButtonRebootToSnapdragon9008.IsEnabled = false;
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            this.Dispatcher.Invoke(() =>
            {
                RefreshStart?.Invoke(this, new EventArgs());
                switch (deviceSimpleInfo.Status)
                {
                    case DeviceStatus.Fastboot:
                        ButtonRebootToBootloader.IsEnabled = true;
                        ButtonRebootToRecovery.IsEnabled = false;
                        ButtonRebootToSystem.IsEnabled = true;
                        ButtonRebootToSnapdragon9008.IsEnabled = true;
                        break;
                    case DeviceStatus.Recovery:
                    case DeviceStatus.Poweron:
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
                RefreshFinished?.Invoke(this, new EventArgs());
            });
        }

        private void ButtonRebootToSystem_Click(object sender, RoutedEventArgs e)
        {
            var fmp = FunctionModuleProxy.Create<RebootOperator>(new RebootArgs(App.SelectedDevice)
            {
                rebootOption = RebootOptions.System,
                nowStatus = App.SelectedDevice.Status
            });
            fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
            fmp.AsyncRun();
        }

        private void ButtonRebootToRecovery_Click(object sender, RoutedEventArgs e)
        {
            var fmp = FunctionModuleProxy.Create<RebootOperator>(new RebootArgs(App.SelectedDevice)
            {
                rebootOption = RebootOptions.Recovery,
                nowStatus = App.SelectedDevice.Status
            });
            fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
            fmp.AsyncRun();
        }

        private void ButtonRebootToBootloader_Click(object sender, RoutedEventArgs e)
        {
            var fmp = FunctionModuleProxy.Create<RebootOperator>(new RebootArgs(App.SelectedDevice)
            {
                rebootOption = RebootOptions.Bootloader,
                nowStatus = App.SelectedDevice.Status
            });
            fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
            fmp.AsyncRun();
        }

        private async void ButtonRebootToSnapdragon9008_Click(object sender, RoutedEventArgs e)
        {
            bool _needToContinue = await Task.Run(() =>
            {
                return UIHelper.ShowChoiceGrid("msgNotice",
                                    UIHelper.GetString("msgNoticeForRebootToEdlLine1") + "\n" +
                                    UIHelper.GetString("msgNoticeForRebootToEdlLine2") + "\n" +
                                     UIHelper.GetString("msgNoticeForRebootToEdlLine3"),
                                    "btnCancel",
                                    "btnContinue"
                    );
            });
            if (!_needToContinue) return;
            var fmp = FunctionModuleProxy.Create<RebootOperator>(new RebootArgs(App.SelectedDevice)
            {
                rebootOption = RebootOptions.Snapdragon9008,
                nowStatus = App.SelectedDevice.Status
            });
            fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
            fmp.AsyncRun();
        }
    }
}
