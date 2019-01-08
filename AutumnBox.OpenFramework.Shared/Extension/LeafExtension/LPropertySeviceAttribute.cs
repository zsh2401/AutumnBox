using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
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
