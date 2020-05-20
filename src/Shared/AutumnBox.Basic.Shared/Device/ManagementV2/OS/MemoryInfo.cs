using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 内存信息
    /// </summary>
    public struct MemoryInfo
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public int Total { get; set; }
        public int Free { get; set; }
        public int Buffers { get; set; }
        public int Available { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
