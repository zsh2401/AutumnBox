/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 17:51:33 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.Depending;
using AutumnBox.GUI.MVVM;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.ViewModel
{
    class VMDeviceDetails : ViewModelBase, ISelectDeviceChangedListener
    {
        public void OnSelectDevice(SelectDeviceEventArgs args)
        {
            Logger.Debug(this,"good");
        }

        public void OnSelectNoDevice()
        {
            Logger.Debug(this,"no");
        }
    }
}
