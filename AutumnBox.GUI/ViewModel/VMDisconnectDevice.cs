/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 1:00:13 (UTC +8:00)
** desc： ...
*************************************************/
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
            var disconnecter = new NetDeviceDisconnecter();
            disconnecter.Init(new Basic.FlowFramework.FlowArgs()
            {
                DevBasicInfo = DeviceSelectionObserver.Instance.CurrentDevice
            });
            disconnecter.RunAsync();
            ViewCloser?.Invoke();
        }
        private void DisconnectAndDisableImpl()
        {
            var fuckYou = new NetDebuggingCloser();
            fuckYou.Init(new Basic.FlowFramework.FlowArgs()
            {
                DevBasicInfo = DeviceSelectionObserver.Instance.CurrentDevice
            });
            fuckYou.Finished += (s, e) =>
            {
                DisconnectImpl();
            };
            fuckYou.RunAsync();
        }
    }
}
