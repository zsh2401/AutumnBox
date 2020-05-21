/*

* ==============================================================================
*
* Filename: ComponentAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 16:14:05
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
    /// 表示组件的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ComponentAttribute : Attribute
    {
        /// <summary>
        /// 是否是单例模式
        /// </summary>
        public bool SingletonMode { get; set; } = true;

        /// <summary>
        /// 通过ID确定该组件
        /// </summary>
        public string Id { get; set; } = null;

        /// <summary>
        /// 通过类型确定该组件
        /// </summary>
        public Type Type { get; set; } = null;
    }
}
