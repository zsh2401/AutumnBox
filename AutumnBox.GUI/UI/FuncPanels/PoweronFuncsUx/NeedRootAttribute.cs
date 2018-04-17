/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/17 19:15:59 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Helper;

namespace AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx
{
    class NeedRootAttribute : FuncsPrecheckAttribute
    {
        public override bool Check(DeviceBasicInfo tragetDevice)
        {
            if (!((MainWindow)App.Current.MainWindow).DevInfoPanel.CurrentDeviceIsRoot)
            {
                return BoxHelper.ShowChoiceDialog("Warning", "warrningNeedRootAccess").ToBool();
            }
            return true;
        }
    }
}
