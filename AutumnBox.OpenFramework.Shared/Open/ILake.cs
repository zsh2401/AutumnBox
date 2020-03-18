/*

* ==============================================================================
*
* Filename: IFactory
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 14:00:13
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 湖
    /// </summary>
    public interface ILake
    {
        /// <summary>
        /// 注册一个工厂
        /// </summary>
        /// <param name="id"></param>
        /// <param name="factory"></param>
        /// <returns>返回自身,以提供链式调用</returns>
        ILake Register(string id, Func<object> factory);
        /// <summary>
        /// 获取一个值
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException">当id无效时发生</exception>
        /// <returns>如果成功,则返回值</returns>
        object Get(string id);
    }
}
