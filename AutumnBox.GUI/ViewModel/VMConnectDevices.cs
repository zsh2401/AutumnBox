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
using AutumnBox.GUI.View.Windows;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.Log;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMConnectDevices : ViewModelBase
    {
        public string DisplayMemeberPath { get; set; } = "Serial";

        public DeviceBasicInfo Selected
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
        private DeviceBasicInfo _selected;

        public IEnumerable<DeviceBasicInfo> Devices
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
                    Selected = DeviceBasicInfo.None;
                }
            }
        }
        private DeviceBasicInfo[] _devices;

        public FlexiableCommand ConnectDevice { get; set; }
        public FlexiableCommand DisconnectDevice { get; set; }
        public FlexiableCommand OpenDeviceNetDebugging { get; set; }

        private void SwitchCommandState()
        {
            DisconnectDevice.CanExecuteProp = Selected.Serial?.IsIPAddress ?? false;
            OpenDeviceNetDebugging.CanExecuteProp = (!Selected.Serial?.IsIPAddress) ?? false;
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
            Logger.Debug(this, "Raise event");
            if (Selected == DeviceBasicInfo.None)
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
            Devices = e.DevicesList;
        }
    }
}
