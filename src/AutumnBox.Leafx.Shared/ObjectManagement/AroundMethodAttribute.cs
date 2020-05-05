/*

* ==============================================================================
*
* Filename: AroundMethodAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:29:05
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;

namespace AutumnBox.Leafx.ObjectManagement
{
    /// <summary>
    /// 表示方法环绕切面的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public abstract class AroundMethodAttribute : Attribute
    {
        /// <summary>
        /// 执行环绕的函数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract object Around(AroundMethodArgs args);
    }
   
}
