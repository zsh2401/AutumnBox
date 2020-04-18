/*

* ==============================================================================
*
* Filename: ClassTextAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/3/7 0:24:18
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.OpenFramework.Open.TextKit
{
    /// <summary>
    /// 类文本
    /// </summary>
    public class ClassTextAttribute : ExtensionI18NTextInfoAttribute
    {
        /// <summary>
        /// 标准格式化字符串
        /// </summary>
        public const string FMT = "ClassTextAttribute-{0}";
        /// <summary>
        /// 根据Id获取具体的键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetKeyById(string id)
        {
            return string.Format(FMT, id);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultText"></param>
        /// <param name="values"></param>
        public ClassTextAttribute(string key, string defaultText, params string[] values) :
            base(defaultText, values)
        {
            this.key = string.Format(FMT, key);
        }

        private readonly string key;

        /// <summary>
        /// 键
        /// </summary>
        public override string Key => key;
    }
}
