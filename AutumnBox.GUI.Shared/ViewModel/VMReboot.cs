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

        private const int SYSTEM = 0;
        private const int FASTBOOT = 1;
        private const int RECOVERY = 2;
        private const int _9008 = 3;
        private const string KEY_REBOOT_OPTION = "reboot_option";

        private void InitCommands()
        {
            _toSystem = new FlexiableCommand(() =>
            {
                var thread = AtmbContext.Instance.NewExtensionThread("ERebooter");
                thread.Data[KEY_REBOOT_OPTION] = SYSTEM;
                thread.Start();
            })
            { CanExecuteProp = false };
            _toRecovery = new FlexiableCommand(() =>
            {
                var thread = AtmbContext.Instance.NewExtensionThread("ERebooter");
                thread.Data[KEY_REBOOT_OPTION] = RECOVERY;
                thread.Start();
            })
            { CanExecuteProp = false }; ;
            _toFastboot = new FlexiableCommand(() =>
            {
                var thread = AtmbContext.Instance.NewExtensionThread("ERebooter");
                thread.Data[KEY_REBOOT_OPTION] = FASTBOOT;
                thread.Start();
            })
            { CanExecuteProp = false }; ;
            _to9008 = new FlexiableCommand(() =>
            {
                var thread = AtmbContext.Instance.NewExtensionThread("ERebooter");
                thread.Data[KEY_REBOOT_OPTION] = _9008;
                thread.Start();
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
