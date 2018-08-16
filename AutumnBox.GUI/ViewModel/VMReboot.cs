/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 18:31:08 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.GUI.Depending;
using AutumnBox.GUI.MVVM;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMReboot : ViewModelBase, ISelectDeviceChangedListener
    {
        private DeviceBasicInfo currentDevice;

        public ICommand ToSystem => _toSystem;
        private readonly FlexiableCommand _toSystem;

        public ICommand ToRecovery => _toRecovery;
        private readonly FlexiableCommand _toRecovery;

        public ICommand ToFastboot => _toFastboot;
        private readonly FlexiableCommand _toFastboot;

        public ICommand To9008 => _to9008;
        private readonly FlexiableCommand _to9008;

        public VMReboot()
        {
            _toSystem = new FlexiableCommand(() =>
            {
                DeviceRebooter.Reboot(currentDevice, RebootOptions.System);
            })
            { CanExecuteProp = false };
            _toRecovery = new FlexiableCommand(() =>
            {
                DeviceRebooter.Reboot(currentDevice, RebootOptions.Recovery);
            })
            { CanExecuteProp = false }; ;
            _toFastboot = new FlexiableCommand(() =>
            {
                DeviceRebooter.Reboot(currentDevice, RebootOptions.Fastboot);
            })
            { CanExecuteProp = false }; ;
            _to9008 = new FlexiableCommand(() =>
            {
                DeviceRebooter.Reboot(currentDevice, RebootOptions.Snapdragon9008);
            })
            { CanExecuteProp = false }; ;
        }

        public void OnSelectNoDevice()
        {
            currentDevice = DeviceBasicInfo.None;
            _to9008.CanExecuteProp = false;
            _toSystem.CanExecuteProp = false;
            _toFastboot.CanExecuteProp = false;
            _toRecovery.CanExecuteProp = false;
        }

        public void OnSelectDevice(SelectDeviceEventArgs args)
        {
            currentDevice = args.DeviceInfo;
            _to9008.CanExecuteProp = true;
            _toSystem.CanExecuteProp = true;
            _toFastboot.CanExecuteProp = true;
            _toRecovery.CanExecuteProp = currentDevice.State != DeviceState.Fastboot;
        }
    }
}
