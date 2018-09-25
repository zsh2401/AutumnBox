/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 9:26:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("免ROOT修改DPI")]
    [ExtName("Modify dpi without root", Lang = "en-US")]
    [ExtIcon("Icons.dpi.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EDpiChanger : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            Ux.Error("此模块尚未完成,请等待");
            //TODO
            return ERR;
        }
    }
}
