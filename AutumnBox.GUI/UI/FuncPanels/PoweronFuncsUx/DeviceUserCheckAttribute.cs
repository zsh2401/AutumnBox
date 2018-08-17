/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/17 18:26:05 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Util.UI;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx
{
    public class DeviceUserCheckAttribute : FuncsPrecheckAttribute
    {
        public override bool Check(DeviceBasicInfo targetDevice)
        {
            var users = new UserManager(targetDevice).GetUsers();
            Logger.Info(this, users.Length);
            if (users.Length > 0)
            {
                return BoxHelper.ShowChoiceDialog("Warning",
                  "msgMaybeHaveOtherUser",
                  "btnCancel", "btnIHaveDeletedAllUser").ToBool();
            }
            return true;
        }
    }
}
