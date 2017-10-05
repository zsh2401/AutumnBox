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
