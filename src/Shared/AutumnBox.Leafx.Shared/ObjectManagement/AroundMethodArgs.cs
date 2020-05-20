/*

* ==============================================================================
*
* Filename: AroundMethodArgs
* Description: 
*
* Version: 1.0
* Created: 2020/4/11 1:01:04
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
    public sealed class AroundMethodArgs
    {
        public MethodInfo MethodInfo { get; internal set; }
        public object Instance { get; internal set; }
        public object[] Args { get; internal set; }
        public Dictionary<string, object> ExtraArgs { get; internal set; }
        internal Func<object> Invoker { get; set; }
        public object Invoke()
        {
            return this.Invoker();
        }
    }
}
