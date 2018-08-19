/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/19 19:47:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Bus
{
    class ConnectedDevicesListener
    {
        public static readonly ConnectedDevicesListener Instance;
        static ConnectedDevicesListener()
        {
            Instance = new ConnectedDevicesListener();
        }
        public IEnumerable<DeviceBasicInfo> Now { get; private set; } = new DeviceBasicInfo[0];
        private readonly DevicesMonitor.DevicesMonitorCore core;
        public event DevicesChangedHandler DevicesChanged
        {
            add
            {
                DeviceChangedSource += value;
                value?.Invoke(this, new DevicesChangedEventArgs(Now));
            }
            remove
            {
                DeviceChangedSource -= value;
            }
        }
        private event DevicesChangedHandler DeviceChangedSource;

        private ConnectedDevicesListener()
        {
            core = new DevicesMonitor.DevicesMonitorCore();
            core.DevicesChanged += ConnectedDevicesListener_DevicesChanged;
        }

        private void ConnectedDevicesListener_DevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            Now = e.DevicesList;
            App.Current.Dispatcher.Invoke(() =>
            {
                DeviceChangedSource?.Invoke(sender, e);
            });
        }

        public void Work()
        {
            core.Begin();
        }
    }
}
