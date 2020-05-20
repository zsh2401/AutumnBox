#nullable enable
using System;
using System.Runtime.Serialization;

namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// 表示组件未能找到的异常
    /// </summary>
    public class ComponentNotFoundException : Exception
    {
        /// <summary>
        /// 初始化ComponentNotFoundException的新实例
        /// </summary>
        public ComponentNotFoundException()
        {
        }

        /// <summary>
        /// 初始化ComponentNotFoundException的新实例
        /// </summary>
        public ComponentNotFoundException(string msg = "Component not found") :
            base(msg)
        { }

        /// <summary>
        /// 初始化ComponentNotFoundException的新实例
        /// </summary>
        public ComponentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 初始化ComponentNotFoundException的新实例
        /// </summary>
        protected ComponentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
