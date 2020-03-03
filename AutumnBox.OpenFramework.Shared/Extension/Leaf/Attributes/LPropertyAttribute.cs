using System;

namespace AutumnBox.OpenFramework.Extension.Leaf.Attributes
{
    /// <summary>
    /// 依赖
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LPropertyAttribute : Attribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        public LPropertyAttribute() { }
    }
}
