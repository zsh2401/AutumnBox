/* =============================================================================*\
*
* Filename: DebugInfo.cs
* Description: 
*
* Version: 1.0
* Created: 9/30/2017 18:31:09(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.SharedTools;

namespace AutumnBox.Basic
{
    internal static class Debug
    {
        internal static LoggerObject Logger = new LoggerObject("basic.log");
        public static readonly bool SHOW_OUTPUT = true;
        public static readonly bool SHOW_COMMAND = true;
    }
}
