using AutumnBox.Basic.Device;

namespace AutumnBox.GUI.UI
{
    internal interface IRefreshable
    {
        void Reset();
        void Refresh(DeviceBasicInfo deviceSimpleInfo);
    }
}
