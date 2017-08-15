using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    internal class Tools
    {
        /// <summary>
        /// 将Bitmap转为BitmapImage
        /// </summary>
        /// <param name="bitmap">一个bitmap对象</param>
        /// <returns>一个BitmapImage对象</returns>
        
        public static string GetHtmlCode(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            //myWebClient.Headers
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
        public static string GetNotice()
        {
            string str = "";
#if DEBUG
            string ggUrl = "../Api/gg.json";
            str = File.ReadAllText(ggUrl);
#else
            string ggUrl = "https://raw.githubusercontent.com/zsh2401/AutumnBox/master/Api/gg.json";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(ggUrl);
            StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding("utf-8"));
            str = sr.ReadToEnd();
            myStream.Close();
#endif
            JObject j = JObject.Parse(str);
            return j["content"].ToString();
        }
    }
}
