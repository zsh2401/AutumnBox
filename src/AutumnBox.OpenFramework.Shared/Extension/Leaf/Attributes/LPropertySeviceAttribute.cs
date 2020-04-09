using System;

namespace AutumnBox.OpenFramework.Extension.Leaf.Attributes
{
    /// <summary>
    /// 依赖服务
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LPropertySeviceAttribute : Attribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="servName"></param>
        public LPropertySeviceAttribute(string servName) { }
    }
}
