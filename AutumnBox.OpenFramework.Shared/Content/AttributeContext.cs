using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Content
{
    /// <summary>
    /// 提供给特性使用的Context
    /// </summary>
    public class AttributeContext : Context
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="sourceAttribute"></param>
        public AttributeContext(Attribute sourceAttribute)
        {
            SourceAttribute = sourceAttribute ?? throw new ArgumentNullException(nameof(sourceAttribute));
        }
        /// <summary>
        /// Src
        /// </summary>
        public Attribute SourceAttribute { get; }
    }
}
