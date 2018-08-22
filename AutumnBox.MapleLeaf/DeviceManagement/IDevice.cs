using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.DeviceManagement
{
    public interface IDevice : IEquatable<IDevice>
    {
        bool IsAlive { get; }
        event EventHandler Disconnect;
        string SerialNumber { get; }
        DeviceState State { get; }
    }
}
