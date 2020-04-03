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

namespace AutumnBox.OpenFramework.Leafx.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute(InjectType injectType=InjectType.ByType) {
            InjectType = injectType;
        }

        public InjectType InjectType { get; }
    }
}
