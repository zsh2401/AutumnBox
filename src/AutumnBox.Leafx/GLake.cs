/*

* ==============================================================================
*
* Filename: GLake
* Description: 
*
* Version: 1.0
* Created: 2020/4/11 1:12:54
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Leafx
{
    public static class GLake
    {
        public static ILake Lake { get; }
        static GLake()
        {
            Lake = new SunsetLake();
        }
    }
}
