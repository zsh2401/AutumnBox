using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;

namespace AutumnBox.GUI.Services
{
    interface IAdbDevicesManager
    {
        IDevice SelectedDevice { get; set; }
        IEnumerable<IDevice> ConnectedDevices { get; }
        event EventHandler DeviceSelectionChanged;
    }
}
