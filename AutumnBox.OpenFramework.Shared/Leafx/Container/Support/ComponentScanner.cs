/*

* ==============================================================================
*
* Filename: ComponentScanner
* Description: 
*
* Version: 1.0
* Created: 2020/4/3 22:44:51
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx.Container.Support
{
    public class ComponentScanner
    {
        private readonly IRegisterableLake lake;
        private readonly string assemblyNamespace;

        public ComponentScanner(IRegisterableLake lake,string assemblyNamespace)
        {
            if (lake is null)
            {
                throw new ArgumentNullException(nameof(lake));
            }

            if (string.IsNullOrEmpty(assemblyNamespace))
            {
                throw new ArgumentException("message", nameof(assemblyNamespace));
            }

            this.lake = lake;
            this.assemblyNamespace = assemblyNamespace;
        }
        public void ScanAndRegister()
        {
        }
    }
}
