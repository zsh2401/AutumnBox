using AutumnBox.OpenFramework.Content;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 所有拓展模块特性的基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class ExtensionAttribute : Attribute
    {
        /// <summary>
        /// 提供的Context
        /// </summary>
        protected AttributeContext Context
        {
            get
            {
                return _lazyCtx.Value;
            }
        }
        private Lazy<AttributeContext> _lazyCtx;
        /// <summary>
        /// 构造与初始化
        /// </summary>
        public ExtensionAttribute()
        {
            _lazyCtx = new Lazy<AttributeContext>(() =>
            {
                return new AttributeContext(this);
            });
        }
    }
}
