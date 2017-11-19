using AutumnBox.Basic.Devices;
using System;

namespace AutumnBox.GUI.UI
{
    public interface IDeviceInfoRefreshable
    {
        event EventHandler RefreshStart;
        event EventHandler RefreshFinished;
        void SetDefault();
        void Refresh(DeviceBasicInfo deviceSimpleInfo);
    }
}
