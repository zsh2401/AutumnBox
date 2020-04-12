/*

* ==============================================================================
*
* Filename: VMDeviceSelector
* Description: 
*
* Version: 1.0
* Created: 2020/3/13 19:14:36
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;

using AutumnBox.Leafx.ObjectManagement;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.GUI.ViewModel
{
    class VMDeviceSelector : ViewModelBase
    {
        public IEnumerable<IDevice> Devices
        {
            get
            {
                return _devs;
            }
            set
            {
                _devs = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<IDevice> _devs;

        public IDevice SelectedDevice
        {
            get
            {
                return _selectedDev;
            }
            set
            {
                _selectedDev = value;
                RaisePropertyChanged();
                SelectionChanged();
            }
        }
        private IDevice _selectedDev;

        [AutoInject]
        private IAdbDevicesManager devicesManager;

        public VMDeviceSelector()
        {
            devicesManager.ConnectedDevicesChanged += (s, e) =>
            {
                Devices = e.Devices;
                if (Devices.Count() >= 1)
                    SelectedDevice = Devices.First();
            };
        }
        private void SelectionChanged()
        {
            if (SelectedDevice != null)
                devicesManager.SelectedDevice = SelectedDevice;
            else
                devicesManager.SelectedDevice = null;
        }
    }
}
