using AutumnBox.Basic.Device;

namespace AutumnBox.GUI.UI
{
    public interface IRefreshable
    {
        void Reset();
        void Refresh(DeviceBasicInfo deviceSimpleInfo);
    }
}
