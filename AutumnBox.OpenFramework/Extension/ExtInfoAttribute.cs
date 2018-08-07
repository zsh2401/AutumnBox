/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 16:00:55 (UTC +8:00)
** desc： ...
*************************************************/
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 所有拓展模块特性的基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class ExtInfoAttribute : Attribute
    {
        /// <summary>
        /// 获取键
        /// </summary>
        public virtual string Key
        {
            get
            {
                string typeName = GetType().Name;
                return typeName.Substring(0, typeName.IndexOf("Attribute"));
            }
        }
        /// <summary>
        /// 获取值
        /// </summary>
        public object Value { get; protected set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public ExtInfoAttribute(object value)
        {
            this.Value = value;
        }
    }
}
