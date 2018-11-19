using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 所有拓展模块特性的基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class ExtensionAttribute : Attribute
    {
    }
}
