/*

* ==============================================================================
*
* Filename: IExtensionAttributeReader
* Description: 
*
* Version: 1.0
* Created: 2020/3/6 20:00:04
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

namespace AutumnBox.OpenFramework.Management.Wrapper
{
    /// <summary>
    /// 拓展模块Attribute读取器
    /// </summary>
    public interface IExtensionAttributeReader
    {
        /// <summary>
        /// 读取引用类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ReadRef<T>(string key) where T : class;
        /// <summary>
        /// 读取值类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ReadVal<T>(string key) where T : struct;
    }
}
