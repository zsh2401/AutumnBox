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
        /// <param name="t"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        ILake Register(Type t, Func<object> factory);
        /// <summary>
        /// 获取一个值
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        object Get(Type t);
    }
}
