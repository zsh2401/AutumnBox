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

        public void Refresh(DeviceSimpleInfo deviceSimpleInfo)
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
            RebootOperator ro = new RebootOperator(new RebootArgs
            {
                rebootOption = RebootOptions.System,
                nowStatus = App.SelectedDevice.Status
            });
            var rm = App.SelectedDevice.GetRunningManger(ro);
            rm.FuncEvents.Finished += App.OwnerWindow.FuncFinish;
            rm.FuncStart();
        }

        private void ButtonRebootToRecovery_Click(object sender, RoutedEventArgs e)
        {
            RebootOperator ro = new RebootOperator(new RebootArgs
            {
                rebootOption = RebootOptions.Recovery,
                nowStatus = App.SelectedDevice.Status
            });
            var rm = App.SelectedDevice.GetRunningManger(ro);
            rm.FuncEvents.Finished += App.OwnerWindow.FuncFinish;
            rm.FuncStart();
        }

        private void ButtonRebootToBootloader_Click(object sender, RoutedEventArgs e)
        {
            RebootOperator ro = new RebootOperator(new RebootArgs
            {
                rebootOption = RebootOptions.Bootloader,
                nowStatus = App.SelectedDevice.Status
            });
            var rm = App.SelectedDevice.GetRunningManger(ro);
            rm.FuncEvents.Finished += App.OwnerWindow.FuncFinish;
            rm.FuncStart();
        }
    }
}
