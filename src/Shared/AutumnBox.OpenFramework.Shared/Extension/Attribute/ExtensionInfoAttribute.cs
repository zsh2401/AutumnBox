#nullable enable
namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展标准信息特性
    /// </summary>
    public abstract class ExtensionInfoAttribute : ExtensionAttribute
    {
        /// <summary>
        /// 键
        /// </summary>
        public virtual string Key { get; }

        /// <summary>
        /// 值
        /// </summary>
        public virtual object? Value { get; }

        /// <summary>
        /// 标准键值对特性
        /// </summary>
        public ExtensionInfoAttribute()
        {
            Key = GetType().Name.TrimEnd("Attribute".ToCharArray());
        }
        /// <summary>
        /// 标准键值对特性
        /// </summary>
        /// <param name="value"></param>
        public ExtensionInfoAttribute(object? value)
        {
            Key = GetType().Name.TrimEnd("Attribute".ToCharArray());
            this.Value = value;
        }
        /// <summary>
        /// 标准键值对特性
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public ExtensionInfoAttribute(string key, object? value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
