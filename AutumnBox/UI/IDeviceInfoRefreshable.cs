using AutumnBox.Basic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.UI
{
    public interface IDeviceInfoRefreshable
    {
        event EventHandler RefreshStart;
        event EventHandler RefreshFinished;
        void SetDefault();
        void Refresh(DeviceBasicInfo deviceSimpleInfo);
    }
}
