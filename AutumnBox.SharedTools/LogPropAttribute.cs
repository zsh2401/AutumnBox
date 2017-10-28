/* =============================================================================*\
*
* Filename: LogPropAttribute
* Description: 
*
* Version: 1.0
* Created: 2017/10/28 15:18:22 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;

namespace AutumnBox.Shared
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LogPropAttribute : Attribute
    {
        public string TAG { get; set; }
    }
}
