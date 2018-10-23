/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 18:31:08 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMReboot : ViewModelBase
    {

        public ICommand ToSystem => _toSystem;
        private FlexiableCommand _toSystem;

        public ICommand ToRecovery => _toRecovery;
        private FlexiableCommand _toRecovery;

        public ICommand ToFastboot => _toFastboot;
        private FlexiableCommand _toFastboot;

        public ICommand To9008 => _to9008;
        private FlexiableCommand _to9008;

        public VMReboot()
        {
            InitCommands();
            InitEvents();
        }
        bool running = false;
        private void InitCommands()
        {
            _toSystem = new FlexiableCommand(() =>
            {
              
                Task.Run(()=> {
                    if (running) return;
                    running = true;
                    DeviceSelectionObserver.Instance.CurrentDevice.Reboot2System();
                    running = false;
                });
            })
            { CanExecuteProp = false };
            _toRecovery = new FlexiableCommand(() =>
            {
                Task.Run(() => {
                    if (running) return;
                    running = true;
                    DeviceSelectionObserver.Instance.CurrentDevice.Reboot2Recovery();
                    running = false;
                });
            })
            { CanExecuteProp = false }; ;
            _toFastboot = new FlexiableCommand(() =>
            {
                Task.Run(() => {
                    if (running) return;
                    running = true;
                    DeviceSelectionObserver.Instance.CurrentDevice.Reboot2Fastboot();
                    running = false;
                });
            })
            { CanExecuteProp = false }; ;
            _to9008 = new FlexiableCommand(() =>
            {
                Task.Run(() => {
                    if (running) return;
                    running = true;
                    DeviceSelectionObserver.Instance.CurrentDevice.Reboot29008();
                    running = false;
                });
            })
            { CanExecuteProp = false };
        }

        private void InitEvents()
        {
            DeviceSelectionObserver.Instance.SelectedDevice += DeviceSelectionChanged;
            DeviceSelectionObserver.Instance.SelectedNoDevice += SelectedNone;
        }

        private void SelectedNone(object sender, System.EventArgs e)
        {
            _to9008.CanExecuteProp = false;
            _toSystem.CanExecuteProp = false;
            _toFastboot.CanExecuteProp = false;
            _toRecovery.CanExecuteProp = false;
        }

        private void DeviceSelectionChanged(object sender, System.EventArgs e)
        {
            _to9008.CanExecuteProp = true;
            _toSystem.CanExecuteProp = true;
            _toFastboot.CanExecuteProp = true;
            _toRecovery.CanExecuteProp = DeviceSelectionObserver.Instance.CurrentDevice?.State != DeviceState.Fastboot;
        }
    }
}
