/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:26:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.OpenFramework.Extension;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("修改DPI", "en-us:Modify dpi without root")]
    [ExtIcon("Icons.dpi.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EDpiModifier : AutumnBoxExtension
    {
        public override int Main(Dictionary<string,object> args)
        {
            string messageOfChoice = CoreLib.Current.Languages.Get("EDpiModiferMessageOfChoice");
            string leftOfChoice = CoreLib.Current.Languages.Get("EDpiModiferLeftChoice");
            string rightOfChoice = CoreLib.Current.Languages.Get("EDpiModiferRightChoice");
            string messageInputNumber = CoreLib.Current.Languages.Get("EDpiModiferHintOfInputNumber");

            var choiceResult = Ux.DoChoice(messageOfChoice,leftOfChoice,rightOfChoice);
            var wm = new WindowManager(TargetDevice);
            switch (choiceResult)
            {
                case OpenFramework.Open.ChoiceResult.Cancel:
                    return ERR_CANCELED_BY_USER;
                case OpenFramework.Open.ChoiceResult.Left:
                    //case OpenFramework.Open.ChoiceResult.Deny:
                    int target = Ux.InputNumber(messageInputNumber, min: 100, max: 1000);
                    wm.Density = target;
                    TargetDevice.Reboot2System();
                    return OK;
                case OpenFramework.Open.ChoiceResult.Right:
                    //case OpenFramework.Open.ChoiceResult.Accept:
                    wm.ResetDensity();
                    TargetDevice.Reboot2System();
                    return OK;
                default:
                    return ERR;
            }
        }
        protected override bool OnStopCommand(object args)
        {
            return false;
        }
    }
}
