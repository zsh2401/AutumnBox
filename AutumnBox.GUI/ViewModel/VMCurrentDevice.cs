/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/20 2:33:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModel
{
    class VMCurrentDevice : ViewModelBase
    {
        public int TabSelectedIndex
        {
            get => selectedIndex; set
            {
                selectedIndex = value;
                RaisePropertyChanged();
            }
        }
        private int selectedIndex;

        public FlexiableCommand PreFunctionPage { get; }
        public FlexiableCommand NextFunctionPage { get; }

        public VMCurrentDevice()
        {
            PreFunctionPage = new FlexiableCommand(() =>
            {
                TabSelectedIndex = (TabSelectedIndex - 1) % 7;
            });
            NextFunctionPage = new FlexiableCommand(() =>
            {
                TabSelectedIndex = (TabSelectedIndex + 1) % 7;
            });
            if (Util.Bus.DeviceSelectionObserver.Instance != null)
            {
                Util.Bus.DeviceSelectionObserver.Instance.SelectedDevice += SelectedDevice;
                Util.Bus.DeviceSelectionObserver.Instance.SelectedNoDevice += NoDevice;
            }
        }

        private void NoDevice(object sender, EventArgs e)
        {

        }

        private void SelectedDevice(object sender, EventArgs e)
        {
            switch (Util.Bus.DeviceSelectionObserver.Instance.CurrentDevice.State)
            {
                case Basic.Device.DeviceState.Poweron:
                    TabSelectedIndex = 0;
                    break;
                case Basic.Device.DeviceState.Recovery:
                    TabSelectedIndex = 1;
                    break;
                case Basic.Device.DeviceState.Fastboot:
                    TabSelectedIndex = 2;
                    break;
                case Basic.Device.DeviceState.Sideload:
                    TabSelectedIndex = 3;
                    break;
                case Basic.Device.DeviceState.Unauthorized:
                    TabSelectedIndex = 4;
                    break;
                case Basic.Device.DeviceState.Offline:
                    TabSelectedIndex = 5;
                    break;
                case Basic.Device.DeviceState.Unknown:
                    TabSelectedIndex = 6;
                    break;
                default:
                    break;
            }
        }
    }
}
