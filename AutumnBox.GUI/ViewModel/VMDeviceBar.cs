using AutumnBox.Basic.Device;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMDeviceBar : ViewModelBase
    {

        public FlexiableCommand ConnectDevice { get; set; }
        public FlexiableCommand DisconnectDevice { get; set; }
        public FlexiableCommand OpenDeviceNetDebugging { get; set; }

        public IEnumerable<IDevice> Devices
        {
            get
            {
                return _devs;
            }
            set
            {
                _devs = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<IDevice> _devs;

        public IDevice SelectedDevice
        {
            get
            {
                return _selectedDev;
            }
            set
            {
                _selectedDev = value;
                RaisePropertyChanged();
                SelectionChanged();
            }
        }
        private IDevice _selectedDev;

        public VMDeviceBar()
        {

            ConnectDevice = new FlexiableCommand((p) =>
            {
                ExtensionBridge.Start("ENetDeviceConnecter");
            });
            DisconnectDevice = new FlexiableCommand((p) =>
            {
                ExtensionBridge.Start("ENetDeviceDisconnecter");
            });
            OpenDeviceNetDebugging = new FlexiableCommand((p) =>
            {
                ExtensionBridge.Start("EOpenUsbDeviceNetDebugging");
            });
            ConnectedDevicesListener.Instance.DevicesChanged += (s, e) =>
            {
                Devices = e.Devices;
                if (Devices.Count() >= 1)
                    SelectedDevice = Devices.First();
            };
            RefreshCommandState();
        }

        private void SelectionChanged()
        {
            if (SelectedDevice != null)
                DeviceSelectionObserver.Instance.RaiseSelectDevice(SelectedDevice);
            else
                DeviceSelectionObserver.Instance.RaiseSelectNoDevice();
            RefreshCommandState();
        }

        private void RefreshCommandState()
        {
            DisconnectDevice.CanExecuteProp = SelectedDevice is NetDevice;
            OpenDeviceNetDebugging.CanExecuteProp = SelectedDevice is UsbDevice;
        }
    }
}
