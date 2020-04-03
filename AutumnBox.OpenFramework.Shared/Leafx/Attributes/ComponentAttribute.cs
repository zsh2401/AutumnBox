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

namespace AutumnBox.OpenFramework.Leafx.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ComponentAttribute : Attribute
    {
        public ComponentAttribute(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            Type = type;
        }

        public Type Type { get; }
    }
}
