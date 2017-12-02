/* =============================================================================*\
*
* Filename: StartWindow
* Description: 
*
* Version: 1.0
* Created: 2017/11/25 1:29:42 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Flows;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using AutumnBox.Basic.Flows.Result;
using AutumnBox.Basic.FlowFramework;

namespace AutumnBox.GUI
{
    partial class MainWindow
    {
        public void FlowFinished(object sender, FinishedEventArgs<FlowResult> e)
        {
            this.Dispatcher.Invoke(() =>
            {
                UIHelper.CloseRateBox();
                switch (sender.GetType().Name)
                {
                    case nameof(IslandActivator):
                    case nameof(IceBoxActivator):
                    case nameof(AirForzenActivator):
                        DevicesOwnerSetted((DeviceOwnerSetter)sender, (DeviceOwnerSetterResult)e.Result);
                        break;
                    default:
                        new FlowResultWindow(e.Result).ShowDialog();
                        break;
                }
            });
        }
        private void DevicesOwnerSetted(DeviceOwnerSetter tor, DeviceOwnerSetterResult result)
        {
            string message = null;
            string advise = null;
            switch (result.ErrorType)
            {
                case Basic.Flows.States.DeviceOwnerSetterErrType.None:
                    break;
                case Basic.Flows.States.DeviceOwnerSetterErrType.DeviceOwnerIsAlreadySet:
                    message = UIHelper.GetString("rmsgDeviceOwnerIsAlreadySet");
                    advise = UIHelper.GetString("advsDeviceOwnerIsAlreadySet");
                    break;
                case Basic.Flows.States.DeviceOwnerSetterErrType.ServalAccountsOnTheDevice:
                    message = UIHelper.GetString("rmsgAlreadyServalAccountsOnTheDevice");
                    advise = UIHelper.GetString("advsAlreadyServalAccountsOnTheDevice");
                    break;
                case Basic.Flows.States.DeviceOwnerSetterErrType.ServalUserOnTheDevice:
                    message = UIHelper.GetString("rmsgAlreadyServalUsersOnTheDevice");
                    advise = UIHelper.GetString("advsAlreadyServalUsersOnTheDevice");
                    break;
                case Basic.Flows.States.DeviceOwnerSetterErrType.UnknowAdmin:
                    message = UIHelper.GetString("rmsgAppHaveNoInstalled");
                    advise = UIHelper.GetString("advsAppHaveNoInstalled");
                    break;
                default:
                    message = UIHelper.GetString("rmsgUnknowError");
                    advise = UIHelper.GetString("advsUnknowError");
                    break;
            }
            new FlowResultWindow(result, message, advise).ShowDialog();
        }
    }
}
