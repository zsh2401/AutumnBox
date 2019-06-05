using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    public struct MemoryInfo
    {
        public int Total { get; set; }
        public int Free { get; set; }
        public int Buffers { get; set; }
        public int Available { get; set; }
    }
}
