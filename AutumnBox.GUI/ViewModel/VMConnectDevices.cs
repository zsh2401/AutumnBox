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
                    SelectedIndex = 0;
                }
            };
            core.Begin();

        }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged();
            }
        }
        private int _selectedIndex;

        public IEnumerable<DeviceBasicInfo> Devices
        {
            get
            {
                return _devices;
            }
            set
            {
                _devices = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<DeviceBasicInfo> _devices;

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
