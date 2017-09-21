using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AutumnBox.SharedTools
{
    public static class NetHelper
    {
        public static string GetHtmlCode(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
    }
}
