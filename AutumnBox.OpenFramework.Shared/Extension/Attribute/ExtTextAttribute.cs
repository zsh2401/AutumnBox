using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 使用该属性,搭配TextAttrManager,可以完美实现多语言文字管理!
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExtTextAttribute : ExtInfoI18NAttribute
    {
        private readonly string key;
        /// <summary>
        /// Key
        /// </summary>
        public override string Key => "ExtText_" + key;
        /// <summary>
        /// 构造,指定键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public ExtTextAttribute(string key, params string[] values) : base(values)
        {
            this.key = key ?? throw new System.ArgumentNullException(nameof(key));
        }
    }
}
