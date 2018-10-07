using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 只要是向设备发出命令实现功能的类，理应该实现此类
    /// </summary>
    public abstract class DeviceCommander : CommandingObject
    {
        public IDevice Device { get; private set; }
        public DeviceCommander(IDevice device)
        {
            this.Device = device;
        }
    }
}
