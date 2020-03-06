/*

* ==============================================================================
*
* Filename: IClassTextLoader
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 16:00:10
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 类文本加载器
    /// </summary>
    public interface IClassTextReader
    {
        /// <summary>
        /// 获取对应类的类文本管理器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IClassTextDictionary Read(Type type);
    }
}
