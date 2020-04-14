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

namespace AutumnBox.Leafx.Container
{
    public interface IRegisterableLake : ILake
    {
        void RegisterComponent(string id,Func<object> factory);
    }
}
