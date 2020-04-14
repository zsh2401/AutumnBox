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
    sealed class AdbDevicesManagerImpl : IAdbDevicesManager,IDisposable
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

        private DevicesMonitor devicesMonitor = new DevicesMonitor();

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

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }
                devicesMonitor.Cancel();
                devicesMonitor = null;
                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~AdbDevicesManagerImpl()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
