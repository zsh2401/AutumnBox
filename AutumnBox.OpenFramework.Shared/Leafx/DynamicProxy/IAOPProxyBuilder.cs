/*

* ==============================================================================
*
* Filename: IAOPProxyBuilder
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:30:39
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx.DynamicProxy
{
    public interface IAOPProxyBuilder
    {
        object Build(Type t);
    }
}
