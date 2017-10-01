namespace AutumnBox.SharedTools
{
    using System.IO;
    using System.Net;
    using System.Text;
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
            sr.Close();
            return strHTML;
        }
    }
}
