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
    public class ShouldAutoCreateException : Exception
    {
        public ShouldAutoCreateException(Type t) {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            T = t;
        }

        public Type T { get; }
    }
}
