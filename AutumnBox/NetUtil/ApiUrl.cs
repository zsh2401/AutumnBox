/* =============================================================================*\
*
* Filename: ApiUrl.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 01:07:24(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.NetUtil
{
    internal static class HelpUrl
    {
        public static readonly string savedevice = "http://api.zsh2401.top/softsupport/autumnbox/help/savedevice";
        public static readonly string flashrecovery = "http://api.zsh2401.top/softsupport/autumnbox/help/flashrecovery";
        public static readonly string flashhelp = "http://api.zsh2401.top/softsupport/autumnbox/help/flashhelp";
    }
    internal static class ApiUrl
    {
        public static readonly string MOTD = "http://api.zsh2401.top/softsupport/autumnbox/motd/";
        public static readonly string Update = "http://api.zsh2401.top/softsupport/autumnbox/update/";
    }
}
