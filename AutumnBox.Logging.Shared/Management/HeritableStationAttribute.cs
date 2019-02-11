using System;

namespace AutumnBox.Logging.Management
{
    /// <summary>
    /// 当ILogStation标记时,将可以继承前任ILogStaion的日志信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HeritableStationAttribute : Attribute
    {
    }
}
