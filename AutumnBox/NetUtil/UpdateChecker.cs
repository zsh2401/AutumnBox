/* =============================================================================*\
*
* Filename: UpdateChecker.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 00:58:05(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.SharedTools;
using AutumnBox.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.NetUtil
{
    public class UpdateCheckFinishedEventArgs : EventArgs
    {
        public string Header { get; set; } = "Ok!";
        public bool NeedUpdate { get; set; } = false;
        public string Version { get; set; }
        public string Message { get; set; }
        public string BaiduPanUrl { get; set; }
        public DateTime Time { get; set; }
        public string GithubReleaseUrl { get; set; }
    }
    public class UpdateChecker : INetUnit
    {
        public event Action<object, UpdateCheckFinishedEventArgs> CheckFinished;
        public void Run()
        {
            try
            {
#if !DEBUG
            string data = NetHelper.GetHtmlCode(ApiUrl.Update);
#else
                string data = File.ReadAllText(@"E:\zsh2401.github.io\softsupport\autumnbox\update\index.html");
#endif
                JObject j = JObject.Parse(data);
                bool needUpdate = (
                    //当前版本小于检测到的版本
                    DebugInfo.NowVersion < new Version(j["Version"].ToString())
                    &&
                    //并且没有被设置跳过
                    new Version(Config.SkipVersion) != new Version(j["Version"].ToString()));
                var e = new UpdateCheckFinishedEventArgs
                {
                    NeedUpdate = needUpdate,
                    Time = new DateTime(Convert.ToInt32(j["Date"][0].ToString()),
                    Convert.ToInt32(j["Date"][1].ToString()),
                    Convert.ToInt32(j["Date"][2].ToString())),
                    Message = j["Message"].ToString(),
                    Version = j["Version"].ToString(),
                    Header = j["Header"].ToString(),
                    BaiduPanUrl = j["BaiduPan"].ToString(),
                    GithubReleaseUrl = j["GithubRelease"].ToString()
                };
                CheckFinished?.Invoke(this, e);
            }
            catch (Exception ex) { Basic.Util.Logger.E(this, "Update Getting Fail", ex); }
        }
    }
}
