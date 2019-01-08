using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
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
