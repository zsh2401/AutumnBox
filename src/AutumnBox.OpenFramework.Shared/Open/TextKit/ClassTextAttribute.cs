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
    public class ClassTextAttribute : ExtTextAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        public ClassTextAttribute(string key, params string[] values) : base(key, values)
        {
        }
    }
}
