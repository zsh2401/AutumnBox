using System;

namespace AutumnBox.OpenFramework.LeafExtension.Attributes
{
    /// <summary>
    /// 可以标记信号接收函数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LSignalReceiveAttribute : Attribute
    {
        /// <summary>
        /// 配对
        /// </summary>
        public string Pattern { get; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="pattern"></param>
        public LSignalReceiveAttribute(string pattern)
        {
            Pattern = pattern;
        }
        /// <summary>
        /// 接收所有消息
        /// </summary>
        public LSignalReceiveAttribute() : this(null)
        {
        }
    }
}
