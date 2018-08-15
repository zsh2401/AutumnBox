/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 18:38:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;

namespace AutumnBox.GUI.UI
{
    public class DeviceChangedEventArgs : EventArgs
    {
        public DeviceBasicInfo CurrentDevice { get; set; }
    }
    public interface INotifyDeviceChanged
    {
        event EventHandler<DeviceChangedEventArgs> DeviceChanged;
        event EventHandler NoDevice;
    }
}
