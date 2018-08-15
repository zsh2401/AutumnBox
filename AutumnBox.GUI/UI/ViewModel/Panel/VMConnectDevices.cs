/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 15:43:27 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.UI.Model;
using AutumnBox.GUI.UI.Model.Panel;
using AutumnBox.GUI.Windows;
using System.ComponentModel;
using System.Windows.Input;

namespace AutumnBox.GUI.UI.ViewModel.Panel
{
    class VMConnectDevices : ViewModelBase
    {
        public ModelPanelDevices Model { get; set; } =  new ModelPanelDevices();
        public ICommand ConnectDevice
            => new MVVMCommand(ConnectDeviceImpl);
        public ICommand DisconnectDevice
            => new MVVMCommand(DisconnectDeviceImpl);
        public ICommand OpenDeviceNetDebugging
            => new MVVMCommand(OpenDeviceNetDebuggingImpl);
        private void ConnectDeviceImpl(object para)
        {
            new DebugWindow().Show();
        }
        private void DisconnectDeviceImpl(object para)
        {
            Model.BtnConnectEnable = !Model.BtnConnectEnable;
            new DebugWindow().Show();
        }
        private void OpenDeviceNetDebuggingImpl(object para)
        {
            new DebugWindow().Show();
        }
    }
}
