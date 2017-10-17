/* =============================================================================*\
*
* Filename: DebugInfo.cs
* Description: 
*
* Version: 1.0
* Created: 10/1/2017 18:38:35(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
#define USE_EN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox
{
    internal static class DebugInfo
    {
        public static readonly DateTime CompiledDate = new DateTime(2017, 10, 17);
        public static Version NowVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
    }
}
