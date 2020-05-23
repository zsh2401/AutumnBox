using AutumnBox.Basic.Device;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using System.Collections.Generic;
using System.Linq;

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

        [AutoInject]
        private readonly IAdbDevicesManager devicesManager;

        [AutoInject]
        private readonly IOpenFxManager openFxManager;

        public VMDeviceBar()
        {
            if (IsDesignMode()) return;
            ConnectDevice = new FlexiableCommand((p) =>
            {
                openFxManager.RunExtension("ENetDeviceConnecter");
            });
            DisconnectDevice = new FlexiableCommand((p) =>
            {
                openFxManager.RunExtension("ENetDeviceDisconnecter");
            });
            OpenDeviceNetDebugging = new FlexiableCommand((p) =>
            {
                openFxManager.RunExtension("EOpenUsbDeviceNetDebugging");
            });
            devicesManager.ConnectedDevicesChanged += (s, e) =>
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
                devicesManager.SelectedDevice = SelectedDevice;
            else
                devicesManager.SelectedDevice = null;
            RefreshCommandState();
        }

        private void RefreshCommandState()
        {
            DisconnectDevice.CanExecuteProp = SelectedDevice is NetDevice;
            OpenDeviceNetDebugging.CanExecuteProp = SelectedDevice is UsbDevice;
        }
    }
}
