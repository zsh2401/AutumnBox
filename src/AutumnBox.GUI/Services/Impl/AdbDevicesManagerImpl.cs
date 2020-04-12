using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.Leafx.Container.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IAdbDevicesManager))]
    sealed class AdbDevicesManagerImpl : IAdbDevicesManager
    {
        public IDevice SelectedDevice
        {
            get => _selectedDevice; set
            {
                _selectedDevice = value;
                DeviceSelectionChanged?.Invoke(this, new EventArgs());
            }
        }
        private IDevice _selectedDevice;

        public IEnumerable<IDevice> ConnectedDevices { get; private set; }

        public event EventHandler DeviceSelectionChanged;

        private readonly DevicesMonitor devicesMonitor = new DevicesMonitor();

        public event DevicesChangedHandler ConnectedDevicesChanged
        {
            add
            {
                devicesMonitor.DevicesChanged += value;
            }
            remove
            {
                devicesMonitor.DevicesChanged -= value;
            }
        }

        public AdbDevicesManagerImpl()
        {
            devicesMonitor.DevicesChanged += DevicesMonitor_DevicesChanged;
        }

        private void DevicesMonitor_DevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            ConnectedDevices = e.Devices;
            if (e.Devices.Any() && SelectedDevice == null)
            {
                SelectedDevice = e.Devices.First();
            }
            else if (!e.Devices.Any())
            {
                SelectedDevice = null;
            }
        }

        public void Initialize()
        {
            devicesMonitor.Start();
        }
    }
}
