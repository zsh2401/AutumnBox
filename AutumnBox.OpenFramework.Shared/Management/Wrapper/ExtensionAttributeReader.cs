/*

* ==============================================================================
*
* Filename: ExtensionInfoReader
* Description: 
*
* Version: 1.0
* Created: 2020/3/6 20:01:51
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Management.Wrapper
{
    public sealed class ExtensionAttributeReader : IExtensionAttributeReader
    {
        public ExtensionAttributeReader(Type extensionType)
        {
        }
        public T ReadRef<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public T ReadVal<T>(string key) where T : struct
        {
            throw new NotImplementedException();
        }
    }
}
