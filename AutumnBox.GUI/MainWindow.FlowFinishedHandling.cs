/* =============================================================================*\
*
* Filename: MainWindow.FlowFinishedHandling
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
using System.Media;
using AutumnBox.GUI.Properties;
using System.Reflection;
using System.Windows.Media;
using AutumnBox.GUI.Util.UI;

namespace AutumnBox.GUI
{
    partial class MainWindow
    {

        private void FlowFinished(object sender, FinishedEventArgs<FlowResult> e)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (Settings.Default.NotifyOnFinish)
                {
                    audioPlayer.Play();
                }
                switch (sender.GetType().Name)
                {
                    case nameof(IslandActivator):
                    case nameof(IceBoxActivator):
                    case nameof(StopAppActivator):
                    case nameof(AirForzenActivator):
                    case nameof(BlackHoleActivator):
                    case nameof(AnzenbokusuActivator):
                    case nameof(FreezeYouActivator):
                    case nameof(AnzenbokusuFakeActivator):
                    case nameof(UsersirActivator):
                        DevicesOwnerSetted((DeviceOwnerSetter)sender, (DeviceOwnerSetterResult)e.Result);
                        break;
                    case nameof(RecoveryFlasher):
                        BoxHelper.ShowMessageDialog("Notice", "msgFlashOK");
                        break;
                    case nameof(FilePusher):
                        PushFinished((AdvanceResult)e.Result);
                        break;
                    default:
                        new FlowResultWindow(e.Result).ShowDialog();
                        break;
                }
            });
        }
        private void PushFinished(AdvanceResult result)
        {
            if (result.ResultType == ResultType.Successful)
            {
                BoxHelper.ShowMessageDialog("Notice", "msgPushOK");
            }
            else
            {
                new FlowResultWindow(result).ShowDialog();
            }
        }
        private void DevicesOwnerSetted(DeviceOwnerSetter tor, DeviceOwnerSetterResult result)
        {
            string message = null;
            string advise = null;
            object rmsgObj = App.Current.Resources[$"rmsgDOS{result.ErrorType.ToString()}"];
            object advsObj = App.Current.Resources[$"advsDOS{result.ErrorType.ToString()}"];
            message = rmsgObj != null ? rmsgObj.ToString() : App.Current.Resources["rmsgUnknowError"].ToString();
            advise = advsObj != null ? advsObj.ToString() : App.Current.Resources["advsUnknowError"].ToString();
            new FlowResultWindow(result, message, advise).ShowDialog();
        }
    }
}
