using AutumnBox.Basic.Device;

namespace AutumnBox.GUI.UI
{
    internal interface IDeviceRefreshable
    {
        void Reset();
        void Refresh(DeviceBasicInfo deviceSimpleInfo);
    }
}
