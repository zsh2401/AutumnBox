using AutumnBox.Basic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Helper
{
    internal interface IChangeByStatus
    {
        bool NeedStatus { get; set; }
        void ChangeByStatus(DeviceStatus nowStatus);
    }
}
