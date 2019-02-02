using System;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    /// <summary>
    /// 有此标记的函数将绝对不可能被扫描为主函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LDoNotScan : Attribute
    {
    }
}
