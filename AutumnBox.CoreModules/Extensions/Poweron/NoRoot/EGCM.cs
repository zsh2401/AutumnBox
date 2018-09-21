using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtRegion("zh-CN")]
    [ExtName("免ROOT激活极客内存清理")]
    [ExtName("Activate GCM without root",Lang ="en-US")]
    [ExtAppProperty("com.ifreedomer.fuckmemory")]
    [ExtIcon("Icons.gcm.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    internal class EGCM : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            if (TargetDevice is NetDevice) {
                Ux.Message(Res("EGCMAlreadyEnableNetDebugging"));
                return OK;
            }
            else if(TargetDevice is UsbDevice usbDevice){
                WriteInitInfo();
                WriteRunning();
                usbDevice.OpenNetDebugging(5555,true);
                return OK;
            }
            Ux.Warn(Res("EGCMNotSupported"));
            return ERR;
        }
    }
}
