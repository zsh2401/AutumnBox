using System;

namespace AutumnBox.OpenFramework.LeafExtension.Attributes
{
    /// <summary>
    /// 有此标记的函数将绝对不可能被扫描为主函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LDoNotScanAttribute : Attribute
    {
    }
}
