/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 15:43:27 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
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

        public ICommand ConnectDevice { get; set; }
        public ICommand DisconnectDevice { get; set; }
        public ICommand OpenDeviceNetDebugging { get; set; }

        public VMConnectDevices()
        {
            ConnectDevice = new MVVMCommand((p) =>
            {
                new DebugWindow().Show();
            });
            DisconnectDevice = new MVVMCommand((p) =>
            {
                new DebugWindow().Show();
            });
            DisconnectDevice = new MVVMCommand((p) =>
            {
                new DebugWindow().Show();
            });
            ConnectedDevicesListener.Instance.DevicesChanged += ConnectedDevicesChanged;
        }

        private void RaiseBusEvent()
        {
            Logger.Debug(this,"Raise event");
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
