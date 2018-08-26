using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.DeviceManagement
{
    public interface IDevice : IEquatable<IDevice>
    {
        string SerialNumber { get; }
        DeviceState State { get; }
        bool IsAlive { get; }
        string Product { get; }
        string Model { get; }
        string Device { get; }
        string TransportId { get; }
    }
}
