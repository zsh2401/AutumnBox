/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 1:00:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using System;

namespace AutumnBox.GUI.ViewModel
{
    class VMDisconnectDevice : ViewModelBase
    {
        public FlexiableCommand Disconnect { get; set; }
        public FlexiableCommand DisconnectAndDisable { get; set; }
        public Action ViewCloser { get; set; }
        public VMDisconnectDevice()
        {
            Disconnect = new FlexiableCommand(DisconnectImpl);
            DisconnectAndDisable = new FlexiableCommand(DisconnectAndDisableImpl);
        }
        private void DisconnectImpl()
        {
            (DeviceSelectionObserver.Instance.CurrentDevice as NetDevice).Disconnect(false);
            ViewCloser?.Invoke();
        }
        private void DisconnectAndDisableImpl()
        {
            (DeviceSelectionObserver.Instance.CurrentDevice as NetDevice).Disconnect(true);
            ViewCloser?.Invoke();
        }
    }
}
