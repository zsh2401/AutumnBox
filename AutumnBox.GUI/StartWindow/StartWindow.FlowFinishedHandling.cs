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
using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.Flows;
using AutumnBox.Basic.FlowFramework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using AutumnBox.Basic.Flows.Result;

namespace AutumnBox.GUI
{
    partial class StartWindow
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
                    message = UIHelper.GetString("msgNoticeDeviceOwnerIsAlreadySet");
                    advise = UIHelper.GetString("advsDeviceOwnerIsAlreadySet");
                    break;
                case Basic.Flows.States.DeviceOwnerSetterErrType.HaveOtherUser:
                    message = UIHelper.GetString("msgNoticeHaveOtherUser");
                    advise = UIHelper.GetString("advIceBoxActHaveOtherUser");
                    break;
                case Basic.Flows.States.DeviceOwnerSetterErrType.UnknowAdmin:
                    message = UIHelper.GetString("msgIceActAppHaveNoInstalled");
                    advise = UIHelper.GetString("advIceActAppHaveNoInstalled");
                    break;
                default:
                    message = UIHelper.GetString("msgUnknowError");
                    advise = UIHelper.GetString("advIceBoxActUnknowError");
                    break;
            }
            new FlowResultWindow(result, message, advise).ShowDialog();
        }
    }
}
