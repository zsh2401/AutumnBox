using AutumnBox.Basic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.UI
{
    public interface IRefreshable
    {
        Action<object> RefreshStart { get; set; }
        Action<object> RefreshFinished { get; set; }
        void Refresh();
        void Refresh(DeviceSimpleInfo deviceSimpleInfo);
    }
}
