/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/19 19:12:03 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;

namespace AutumnBox.GUI.Util.Bus
{
    public class DeviceSelectionObserver
    {
        public static readonly DeviceSelectionObserver Instance;
        static DeviceSelectionObserver()
        {
            Instance = new DeviceSelectionObserver();
        }
        private DeviceSelectionObserver() { }
        public void RaiseSelectDevice(IDevice device)
        {
            CurrentDevice = device;
            App.Current.Dispatcher.Invoke(() =>
            {
                SelectedDeviceSource?.Invoke(this, new EventArgs());
            });
        }
        public void RaiseSelectNoDevice()
        {
            CurrentDevice = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                SelectedNoDeviceSource?.Invoke(this, new EventArgs());
            });

        }
        public bool IsSelectedDevice
        {
            get
            {
                return CurrentDevice != null;
            }
        }

        public event EventHandler SelectedNoDevice
        {
            add
            {
                if (!IsSelectedDevice)
                {
                    value(this, new EventArgs());
                }
                SelectedNoDeviceSource += value;
            }
            remove
            {
                SelectedNoDeviceSource -= value;
            }
        }
        private event EventHandler SelectedNoDeviceSource;

        public event EventHandler SelectedDevice
        {
            add
            {
                if (IsSelectedDevice)
                {
                    value(this, new EventArgs());
                }
                SelectedDeviceSource += value;
            }
            remove
            {
                SelectedDeviceSource -= value;
            }
        }
        private event EventHandler SelectedDeviceSource;
        public IDevice CurrentDevice { get; private set; } = null;
    }
}
