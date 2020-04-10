/*

* ==============================================================================
*
* Filename: AroundMethodAttribute
* Description: 
*
* Version: 1.0
* Created: 2020/3/30 13:29:05
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.Leafx.ObjectManagement
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public abstract class AroundMethodAttribute : Attribute
    {
        public abstract object Around(AroundMethodArgs args);
    }
   
}
