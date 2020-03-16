/*

* ==============================================================================
*
* Filename: InjectAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/3/9 0:36:40
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using System;

namespace AutumnBox.OpenFramework.Open.ProxyKit
{
    /// <summary>
    /// 使用在属性上时,将注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
    }
}
