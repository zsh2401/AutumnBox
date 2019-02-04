/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 15:43:27 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.View.DialogContent;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AutumnBox.GUI.ViewModel
{
    class VMConnectDevices : ViewModelBase
    {
        public Visibility NoDeviceVisibility
        {
            get => _noDevV; set
            {
                _noDevV = value;
                RaisePropertyChanged();
                //if (value == Visibility.Visible) ListVisibility = Visibility.Hidden;
                //else ListVisibility = Visibility.Visible;
            }
        }
        private Visibility _noDevV = Visibility.Visible;

        public Visibility ListVisibility
        {
            get => _listV; set
            {
                _listV = value;
                RaisePropertyChanged();
                if (value == Visibility.Visible) NoDeviceVisibility = Visibility.Hidden;
                else NoDeviceVisibility = Visibility.Visible;
            }
        }
        private Visibility _listV = Visibility.Hidden;

        public string DisplayMemeberPath { get; set; } = nameof(IDevice.SerialNumber);

        public IDevice Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                RaisePropertyChanged();
                RaiseBusEvent();
                SwitchCommandState();
            }
        }
        private IDevice _selected;

        public IEnumerable<IDevice> Devices
        {
            get
            {
                return _devices;
            }
            set
            {
                _devices = value.ToArray();
                RaisePropertyChanged();
                if (value.Count() > 0)
                {
                    Selected = _devices[0];
                    ListVisibility = Visibility.Visible;
                }
                else
                {
                    Selected = null;
                    ListVisibility = Visibility.Hidden;
                }
            }
        }
        private IDevice[] _devices;

        public FlexiableCommand ConnectDevice { get; set; }
        public FlexiableCommand DisconnectDevice { get; set; }
        public FlexiableCommand OpenDeviceNetDebugging { get; set; }

        private void SwitchCommandState()
        {
            DisconnectDevice.CanExecuteProp = Selected is NetDevice;
            OpenDeviceNetDebugging.CanExecuteProp = Selected is UsbDevice;
        }

        public VMConnectDevices()
        {
            ConnectDevice = new FlexiableCommand((p) =>
            {
                AtmbContext.Instance.NewExtensionThread("ENetDeviceConnecter")?.Start();
            });
            DisconnectDevice = new FlexiableCommand((p) =>
            {
                AtmbContext.Instance.NewExtensionThread("ENetDeviceDisconnecter")?.Start();
            });
            OpenDeviceNetDebugging = new FlexiableCommand((p) =>
            {
                AtmbContext.Instance.NewExtensionThread("EOpenUsbDeviceNetDebugging")?.Start();
            });
            ConnectedDevicesListener.Instance.DevicesChanged += ConnectedDevicesChanged;
        }

        private void RaiseBusEvent()
        {
            if (Selected == null)
            {
                DeviceSelectionObserver.Instance.RaiseSelectNoDevice();
            }
            else
            {
                DeviceSelectionObserver.Instance.RaiseSelectDevice(Selected);
            }
        }


        private void ConnectedDevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            Devices = e.Devices;
        }
    }
}
