/*

* ==============================================================================
*
* Filename: ClassTextReader
* Description: 
*
* Version: 1.0
* Created: 2020/3/17 0:25:34
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Implementation
{
    [Component(Type = typeof(IClassTextReader))]
    class ClassTextReader : IClassTextReader
    {
        public IClassTextDictionary Read(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return new ClassTextDictionary(type);
        }

        public IClassTextDictionary Read(object instance)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return Read(instance.GetType());
        }
    }
}
