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

namespace AutumnBox.Leafx.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoInjectAttribute : Attribute
    {
        public string Id { get; set; } = null;
    }
}
