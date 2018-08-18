/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 17:51:33 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.GUI.Depending;
using AutumnBox.GUI.MVVM;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.ViewModel
{
    class VMDeviceDetails : ViewModelBase, ISelectDeviceChangedListener
    {
        public DeviceBasicInfo CurrentDevice { private get; set; }

        public void OnSelectDevice()
        {
            Logger.Debug(this,"good");
        }

        public void OnSelectNoDevice()
        {
            
        }
    }
}
