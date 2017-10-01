using AutumnBox.SharedTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.NetUtil
{
    public class UpdateCheckFinishedEventArgs:EventArgs {
        public bool NeedUpdate { get; set; } = false;
        public string Version { get; set; }
        public string Message { get; set; }
        public string BaiduPanUrl { get; set; }
        public DateTime Time { get; set; }
        public string GithubReleaseUrl { get; set; }
    }
    public class UpdateChecker
    {
        public void Run(Action<object,UpdateCheckFinishedEventArgs> CheckFinished) {

#if !DEBUG
            string data = NetHelper.GetHtmlCode(ApiUrl.Update);
#else
            string data = File.ReadAllText(@"E:\zsh2401.github.io\softsupport\autumnbox\update\index.html");
#endif
            JObject j = JObject.Parse(data);
            var e = new UpdateCheckFinishedEventArgs
            {
                NeedUpdate = new Version(StaticData.nowVersion.version) <= new Version(j["Version"].ToString()),
                Time = new DateTime(Convert.ToInt32(j["Date"][0].ToString()),
                Convert.ToInt32(j["Date"][1].ToString()),
                Convert.ToInt32(j["Date"][2].ToString())),
                Message = j["Message"].ToString(),
                BaiduPanUrl = j["BaiduPan"].ToString(),
                GithubReleaseUrl = j["GithubRelease"].ToString()
            };
            CheckFinished?.Invoke(this, e);
        }
    }
}
