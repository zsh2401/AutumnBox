/* =============================================================================*\
*
* Filename: NetHelper.cs
* Description: 
*
* Version: 1.0
* Created: 9/20/2017 19:24:26(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.SharedTools
{
    using System.IO;
    using System.Net;
    using System.Text;
    public static class NetHelper
    {
        public static string GetHtmlCode(string url)
        {
            StreamReader sr = new StreamReader(new WebClient().OpenRead(url), Encoding.GetEncoding("utf-8"));
            string strHTML = sr.ReadToEnd();
            sr.Close();
            return strHTML;
        }
    }
}
