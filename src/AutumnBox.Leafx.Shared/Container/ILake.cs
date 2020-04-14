/*

* ==============================================================================
*
* Filename: ILake
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:17:27
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using System;

namespace AutumnBox.Leafx.Container
{
    public interface ILake
    {
        int Size { get; }
        object GetComponent(string id);
    }
}
