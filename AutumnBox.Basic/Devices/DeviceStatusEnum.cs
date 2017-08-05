using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 设备状态枚举
    /// </summary>
    public enum DeviceStatus
    {
        NO_DEVICE = -1,
        RUNNING,
        RECOVERY,
        FASTBOOT,
    }
}
