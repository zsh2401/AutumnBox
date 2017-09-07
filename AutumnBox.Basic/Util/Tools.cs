using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace AutumnBox.Basic.Util
{
    public static class Tools
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
    internal class Guider
    {
        private const string GUIDE_URL = "https://zsh2401.github.io/autumnbox/api/guide.json";//引导链接

        private JObject sourceData;
        private const string TAG = "Guider";
        public bool isOk { get; private set; }
        public Guider()
        {
            try
            {
                sourceData = GetSourceData(GUIDE_URL);
                Logger.D(TAG, "Init suc");
                isOk = true;
            }
            catch (Exception e)
            {
                Logger.D(TAG, "Get guide fail");
                Logger.D(TAG, e.Message);
                sourceData = JObject.Parse("{\"ok\":\"false\"}");
                isOk = false;
            }
        }
        public JToken this[object index]
        {
            get
            {
                return sourceData[index];
            }
        }
        private JObject GetSourceData(string url)
        {
            return JObject.Parse(Tools.GetHtmlCode(url));
        }
    }
}
