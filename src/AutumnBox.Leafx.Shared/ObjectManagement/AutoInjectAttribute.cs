/*

* ==============================================================================
*
* Filename: Inject
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:27:54
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
    /// 表示自动注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class AutoInjectAttribute : Attribute
    {
        /// <summary>
        /// 表示组件的ID,如果未指定,将根据成员类型注入
        /// </summary>
        public string Id { get; set; } = null;
    }
}
