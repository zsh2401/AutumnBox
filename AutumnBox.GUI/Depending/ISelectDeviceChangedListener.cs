/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/16 13:35:00 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;

namespace AutumnBox.GUI.Depending
{
    public class SelectDeviceEventArgs : EventArgs
    {
        public DeviceBasicInfo DeviceInfo;
    }
    interface ISelectDeviceChangedListener
    {
        void OnSelectNoDevice();
        void OnSelectDevice(SelectDeviceEventArgs args);
    }
}
