using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Operating
{
    public interface IHardwareInfoGetter
    {
        int BatteryLevel { get; }
        int SizeofRam { get; }
        int SizeofRom { get; }
        string Screen { get; }
    }
}
