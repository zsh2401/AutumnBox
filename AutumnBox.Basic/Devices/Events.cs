namespace AutumnBox.Basic.Devices
{
    using System;
    public delegate void DevicesChangedHandler(object sender, DevicesChangeEventArgs e);
    public class DevicesChangeEventArgs : EventArgs
    {
        public DevicesList DevicesList { get; }
        public DevicesChangeEventArgs(DevicesList devList)
        {
            DevicesList = devList;
        }
    }
}
