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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace AutumnBox.GUI.NetUtil
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UpdateCheckResult
    {
        [JsonProperty("header")]
        public string Header { get; set; } = "Ok!";
        [JsonProperty("version")]
        public string VersionString { get; set; } = "0.0.0";
        [JsonProperty("message")]
        public string Message { get; set; } = "No update";
        [JsonProperty("baiduPanUrl")]
        public string BaiduPanUrl { get; set; } = "http://www.baidu.com";
        [JsonProperty("githubReleaseUrl")]
        public string GithubReleaseUrl { get; set; } = "https://github.com/zsh2401/";
        [JsonProperty("time")]
        public int[] TimeArray { get; set; }

        public Version Version => new Version(VersionString);
        public bool NeedUpdate =>
            Version > Helper.SystemHelper.CurrentVersion //检测到的版本大于当前版本
            && new Version(Config.SkipVersion) != Version;//并且没有被设置跳过
        public DateTime Time => new DateTime(TimeArray[0], TimeArray[1], TimeArray[0]);
    }
    [LogProperty(TAG = "Update Check", Show = true)]
    internal class UpdateChecker : RemoteDataGetter<UpdateCheckResult>
    {
#if USE_LOCAL_API&& DEBUG
        public override UpdateCheckResult Get()
        {
            JObject j = JObject.Parse(File.ReadAllText(@"..\docs\api\update\index.html"));
            var result = (UpdateCheckResult)JsonConvert.DeserializeObject(j.ToString(), typeof(UpdateCheckResult));
            return result;
        }
#else
        public override UpdateCheckResult Get()
        {
            Logger.D("Getting update info....");
            byte[] bytes = new WebClient().DownloadData(Urls.UPDATE_API);
            string data = Encoding.UTF8.GetString(bytes);
            Logger.D(data);
            JObject j = JObject.Parse(data);
            var result = (UpdateCheckResult)JsonConvert.DeserializeObject(j.ToString(), typeof(UpdateCheckResult));
            Logger.D("Geted " + data);
            return result;
        }
#endif
    }
}
