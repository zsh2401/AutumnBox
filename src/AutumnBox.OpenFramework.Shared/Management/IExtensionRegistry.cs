/*

* ==============================================================================
*
* Filename: IRegistry
* Description: 
*
* Version: 1.0
* Created: 2020/8/7 22:33:13
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Management
{
    public interface IExtensionRegistry :  ICollection<IRegisteredExtensionInfo>
    {
    }
}
