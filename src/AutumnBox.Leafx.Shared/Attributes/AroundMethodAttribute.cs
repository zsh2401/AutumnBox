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

namespace AutumnBox.Leafx.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class AroundMethodAttribute : Attribute
    {
        public abstract object Around(AroundAspectArgs method);
    }
    public sealed class AroundAspectArgs
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
