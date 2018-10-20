using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 包含单信息的Attribute
    /// </summary>
    public abstract class SingleInfoAttribute : ExtensionAttribute, IInformationAttribute
    {
        /// <summary>
        /// Key
        /// </summary>
        public virtual string Key => GetType().Name;
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; protected set; }
        /// <summary>
        /// 根据值构造
        /// </summary>
        /// <param name="value"></param>
        public SingleInfoAttribute(object value)
        {
            this.Value = value;
        }
    }
}
