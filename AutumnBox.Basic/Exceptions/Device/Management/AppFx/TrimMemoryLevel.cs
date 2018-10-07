using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 内存收紧级别
    /// </summary>
    public enum TrimMemoryLevel
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        HIDDEN,
        RUNNING_MODERATE,
        BACKGROUND,
        RUNNING_LOW,
        MODERATE,
        RUNNING_CRITICAL,
        COMPLETE
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
