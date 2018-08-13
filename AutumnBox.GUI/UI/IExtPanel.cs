/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 5:35:30 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Warpper;

namespace AutumnBox.GUI.UI
{
    interface IExtPanel
    {
        void Set(IExtensionWarpper[] warppers);
        void Set(IExtensionWarpper[] warppers,DeviceBasicInfo currentDevice);
    }
}
