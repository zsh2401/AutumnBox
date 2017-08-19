using AutumnBox.Basic.DebugTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic
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
                Log.d(TAG, "Init suc");
                isOk = true;
            }
            catch (Exception e)
            {
                Log.d(TAG, "Get guide fail");
                Log.d(TAG, e.Message);
                sourceData = JObject.Parse("{\"ok\":\"false\"}");
                isOk = false;
                Log.d(TAG, "Init Fail");
                Log.d(TAG, e.Message);
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
