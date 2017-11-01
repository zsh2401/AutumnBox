/* =============================================================================*\
*
* Filename: CompiledDateAttribute
* Description: 
*
* Version: 1.0
* Created: 2017/10/30 12:27:16 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;

namespace AutumnBox.Support.CstmDebug
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class CompiledDateAttribute : Attribute
    {
        public DateTime DateTime { get; private set; }
        public CompiledDateAttribute(int year, int month, int day, int hours, int min, int sec)
        {
            DateTime = new DateTime(year, month, day, hours, min, sec);
        }
        public CompiledDateAttribute(int y = 2401, int m = 39, int d = 24)
        {
            DateTime = new DateTime(y, m, d);
        }
    }
}
