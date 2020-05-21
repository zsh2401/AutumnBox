/*

* ==============================================================================
*
* Filename: ShouldAutoCreateException
* Description: 
*
* Version: 1.0
* Created: 2020/4/11 2:12:56
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;

namespace AutumnBox.Leafx.Container.Support
{
    /// <summary>
    /// 用于指示工厂函数应转为自动创建
    /// </summary>
    public class ShouldAutoCreateException : Exception
    {
        /// <summary>
        /// 初始化ShouldAutoCreateException的新实例
        /// </summary>
        /// <param name="t"></param>
        public ShouldAutoCreateException(Type t) {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            Type = t;
        }

        /// <summary>
        /// 组件类型
        /// </summary>
        public Type Type { get; }
    }
}
