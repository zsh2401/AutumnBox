/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/17 18:25:28 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.GUI.Util.UI;

namespace AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx
{
    public class TipAttribute : FuncsPrecheckAttribute
    {
        public readonly string MsgKey;
        public TipAttribute(string msg)
        {
            this.MsgKey = msg;
        }

        public override bool Check(DeviceBasicInfo tragetDevice)
        {
            return BoxHelper.ShowChoiceDialog("titleMustRead", MsgKey).ToBool();
        }
    }
}
