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
using AutumnBox.GUI.Cfg;
using AutumnBox.Support.CstmDebug;
using AutumnBox.Support.Helper;
using Newtonsoft.Json.Linq;
using System;

namespace AutumnBox.GUI.NetUtil
{
    public class UpdateCheckResult : NetUtilResult
    {
        public string Header { get; set; } = "Ok!";
        public bool NeedUpdate { get; set; } = false;
        public string Version { get; set; }
        public string Message { get; set; }
        public string BaiduPanUrl { get; set; }
        public DateTime Time { get; set; }
        public string GithubReleaseUrl { get; set; }
    }
    [LogProperty(TAG = "Update Check", Show = false)]
    [NetUtilProperty(UseLocalApi = false, MustAddFininshedEventHandler = true)]
    internal class UpdateChecker : NetUtil, INetUtil
    {
        public override NetUtilResult LocalMethod()
        {
            throw new NotImplementedException();
        }

        public override NetUtilResult NetMethod()
        {
            Logger.D("Start GET");
            string data = NetHelper.GetHtmlCode(Urls.UPDATE_API);
            Logger.D("Get code finish " + data);
            JObject j = JObject.Parse(data);
            bool needUpdate = (
                //当前版本小于检测到的版本
                Helper.SystemHelper.CurrentVersion < new Version(j["Version"].ToString())
                &&
                //并且没有被设置跳过
                new Version(Config.SkipVersion) != new Version(j["Version"].ToString()));
            var e = new UpdateCheckResult
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
            Logger.D("get from net were success!");
            return e;
        }
    }
}
