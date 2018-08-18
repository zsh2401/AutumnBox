/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 13:35:00 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;

namespace AutumnBox.GUI.Depending
{
    interface ISelectDeviceChangedListener
    {
        DeviceBasicInfo CurrentDevice { set; }
        void OnSelectNoDevice();
        void OnSelectDevice();
    }
}
