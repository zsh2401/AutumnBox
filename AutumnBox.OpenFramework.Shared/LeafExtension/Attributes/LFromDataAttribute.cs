using System;

namespace AutumnBox.OpenFramework.LeafExtension.Attributes
{
    /// <summary>
    /// Main函数中进行此标记的参数将会根据KEY从Data中获取值
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class LFromDataAttribute : Attribute
    {
        /// <summary>
        /// Main函数中进行此标记的参数将会根据KEY从Data中获取值
        /// </summary>
        /// <param name="key"></param>
        public LFromDataAttribute(string key)
        {
            Key = key;
        }
        /// <summary>
        /// Main函数中进行此标记的参数将会根据其参数名从Data中获取值
        /// </summary>
        public LFromDataAttribute()
        {
        }
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; }
    }
}
