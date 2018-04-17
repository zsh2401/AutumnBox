/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/17 18:24:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;

namespace AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx
{
    public abstract class FuncsPrecheckAttribute : Attribute
    {
        public abstract bool Check(DeviceBasicInfo tragetDevice);
    }
}
