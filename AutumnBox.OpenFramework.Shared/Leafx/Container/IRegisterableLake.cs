/*

* ==============================================================================
*
* Filename: IRegisterableLake
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:24:43
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Leafx
{
    public interface IRegisterableLake : ILake
    {
        void Register(string id,Func<object> factory);
    }
}
