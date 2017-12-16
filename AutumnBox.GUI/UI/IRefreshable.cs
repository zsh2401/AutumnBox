using AutumnBox.Basic.Devices;
using System;

namespace AutumnBox.GUI.UI
{
    public interface IRefreshable
    {
        void Reset();
        void Refresh(DeviceBasicInfo deviceSimpleInfo);
    }
}
