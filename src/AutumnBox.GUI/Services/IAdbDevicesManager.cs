using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using System;
using System.Collections.Generic;

namespace AutumnBox.GUI.Services
{
    interface IAdbDevicesManager
    {
        IDevice SelectedDevice { get; set; }
        void Initialize();
        IEnumerable<IDevice> ConnectedDevices { get; }
        event DevicesChangedHandler ConnectedDevicesChanged;
        event EventHandler DeviceSelectionChanged;
    }
}
