/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 15:43:27 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMConnectDevices : ViewModelBase
    {
        private DevicesMonitor.DevicesMonitorCore core = new DevicesMonitor.DevicesMonitorCore();
        public void Work()
        {
            core.DevicesChanged += (s, e) =>
            {
                Devices = e.DevicesList;
                if (Devices.Count() > 0)
                {
                    Selected = _devices[0];
                }
                else
                {
                    Selected = DeviceBasicInfo.None;
                }
            };
            core.Begin();

        }
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
        }
    }
}
