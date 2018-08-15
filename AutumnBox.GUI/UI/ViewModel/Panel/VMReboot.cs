/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 18:31:08 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.GUI.UI.Model.Panel;
using System.Windows.Input;

namespace AutumnBox.GUI.UI.ViewModel.Panel
{
    class VMReboot : ViewModelBase
    {
        public ModelReboot Model { get; set; } = new ModelReboot();
        public ICommand ToSystem => new MVVMCommand((args) =>
        {
            DeviceRebooter.Reboot(Model.CurrentDevice, RebootOptions.System);
        });
        public ICommand ToRecovery => new MVVMCommand((args) =>
        {
            DeviceRebooter.Reboot(Model.CurrentDevice, RebootOptions.Recovery);
        });
        public ICommand ToFastboot => new MVVMCommand((args) =>
        {
            DeviceRebooter.Reboot(Model.CurrentDevice, RebootOptions.Fastboot);
        });
        public ICommand To9008 => new MVVMCommand((args) =>
        {
            DeviceRebooter.Reboot(Model.CurrentDevice, RebootOptions.Snapdragon9008);
        });
    }
}
