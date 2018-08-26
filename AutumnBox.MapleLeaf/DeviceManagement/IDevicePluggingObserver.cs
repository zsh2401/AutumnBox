/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 2:35:56 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.MapleLeaf.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Multidevice
{
    public class DevicesChangedEventArgs : EventArgs
    {
        public IEnumerable<IDevice> Devices { get; private set; }
        public DevicesChangedEventArgs(IEnumerable<IDevice> devices)
        {
            Devices = devices;
        }
    }
    public interface IDevicePluggingObserver : IDisposable
    {
        event EventHandler<DevicesChangedEventArgs> DevicesChanged;
        void Start();
        void Stop();
    }
}
