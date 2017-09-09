using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Text;

namespace AutumnBox.Basic.Util
{
    internal static class Tools
    {
        /// <summary>
        /// 获取网页代码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        internal static string GetHtmlCode(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }
        /// <summary>
        /// 杀死进程和他的子进程
        /// Code from https://stackoverflow.com/questions/30249873/process-kill-doesnt-seem-to-kill-the-process
        /// </summary>
        /// <param name="pid"></param>
        internal static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();
            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }

            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); //kill child processes(also kills childrens of childrens etc.)
                }
            }
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
