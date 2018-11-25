using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Running
{
    /// <summary>
    /// 拓展模块退出码
    /// </summary>
    public enum ExtensionExitCodes : int
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释\
        Killed = -4,
        CanceledByUser = -3,
        Exception = -2,
        ErrorUnknown = -1,
        Ok = 0,
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }
}
