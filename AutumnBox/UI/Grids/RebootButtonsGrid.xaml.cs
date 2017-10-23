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

namespace AutumnBox.UI.Grids
{
    /// <summary>
    /// RebootButtonsGrid.xaml 的交互逻辑
    /// </summary>
    public partial class RebootButtonsGrid : Grid, IDeviceInfoRefreshable
    {
        public RebootButtonsGrid()
        {
            InitializeComponent();
        }

        public event EventHandler RefreshStart;
        public event EventHandler RefreshFinished;

        public void SetDefault()
        {
            buttonRebootToBootloader.IsEnabled = false;
            buttonRebootToRecovery.IsEnabled = false;
            buttonRebootToSystem.IsEnabled = false;
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            this.Dispatcher.Invoke(() =>
            {
                RefreshStart?.Invoke(this, new EventArgs());
                switch (deviceSimpleInfo.Status)
                {
                    case DeviceStatus.FASTBOOT:
                        buttonRebootToBootloader.IsEnabled = true;
                        buttonRebootToRecovery.IsEnabled = false;
                        buttonRebootToSystem.IsEnabled = true;
                        break;
                    case DeviceStatus.RECOVERY:
                    case DeviceStatus.RUNNING:
                        buttonRebootToBootloader.IsEnabled = true;
                        buttonRebootToRecovery.IsEnabled = true;
                        buttonRebootToSystem.IsEnabled = true;
                        break;
                    default:
                        buttonRebootToBootloader.IsEnabled = false;
                        buttonRebootToRecovery.IsEnabled = false;
                        buttonRebootToSystem.IsEnabled = false;
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
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
        }

        private void ButtonRebootToRecovery_Click(object sender, RoutedEventArgs e)
        {
            var fmp = FunctionModuleProxy.Create<RebootOperator>(new RebootArgs(App.SelectedDevice)
            {
                rebootOption = RebootOptions.Recovery,
                nowStatus = App.SelectedDevice.Status
            });
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
        }

        private void ButtonRebootToBootloader_Click(object sender, RoutedEventArgs e)
        {
            var fmp = FunctionModuleProxy.Create<RebootOperator>(new RebootArgs(App.SelectedDevice)
            {
                rebootOption = RebootOptions.Bootloader,
                nowStatus = App.SelectedDevice.Status
            });
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
        }
    }
}
