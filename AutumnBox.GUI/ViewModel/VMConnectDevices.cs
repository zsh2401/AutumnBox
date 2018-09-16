/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 15:43:27 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.View.DialogContent;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.GUI.ViewModel
{
    class VMConnectDevices : ViewModelBase
    {
        public string DisplayMemeberPath { get; set; } = "SerialNumber";

        public IDevice Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                RaisePropertyChanged();
                RaiseBusEvent();
                SwitchCommandState();
            }
        }
        private IDevice _selected;

        public IEnumerable<IDevice> Devices
        {
            get
            {
                return _devices;
            }
            set
            {
                _devices = value.ToArray();
                RaisePropertyChanged();
                if (value.Count() > 0)
                {
                    Selected = _devices[0];
                }
                else
                {
                    Selected = null;
                }
            }
        }
        private IDevice[] _devices;

        public FlexiableCommand ConnectDevice { get; set; }
        public FlexiableCommand DisconnectDevice { get; set; }
        public FlexiableCommand OpenDeviceNetDebugging { get; set; }

        private void SwitchCommandState()
        {
            DisconnectDevice.CanExecuteProp = Selected is NetDevice;
            OpenDeviceNetDebugging.CanExecuteProp = Selected is UsbDevice;
        }

        public VMConnectDevices()
        {
            ConnectDevice = new FlexiableCommand((p) =>
            {
                new ContentConnectNetDevice().Show();
            });
            DisconnectDevice = new FlexiableCommand((p) =>
            {
                new ContentDisconnectDevice().Show();
            });
            OpenDeviceNetDebugging = new FlexiableCommand((p) =>
            {
                new ContentEnableDeviceNetDebugging().Show();
            });
            ConnectedDevicesListener.Instance.DevicesChanged += ConnectedDevicesChanged;
        }

        private void RaiseBusEvent()
        {
            if (Selected == null)
            {
                DeviceSelectionObserver.Instance.RaiseSelectNoDevice();
            }
            else
            {
                DeviceSelectionObserver.Instance.RaiseSelectDevice(Selected);
            }
        }

        private void ConnectedDevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            Devices = e.Devices;
        }
    }
}
