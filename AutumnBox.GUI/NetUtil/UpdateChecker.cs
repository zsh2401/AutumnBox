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
using AutumnBox.GUI.Properties;
using AutumnBox.Support.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace AutumnBox.GUI.NetUtil
{
    [JsonObject(MemberSerialization.OptOut)]
    internal class UpdateCheckResult
    {
        [JsonProperty("header")]
        public string Header { get; set; } = "Ok!";
        [JsonProperty("version")]
        public string VersionString { get; set; } = "0.0.0";
        [JsonProperty("message")]
        public string Message { get; set; } = "No update";
        [JsonProperty("updateUrl")]
        public string UpdateUrl { get; set; } = "http://atmb.top/";
        [JsonProperty("date")]
        public int[] TimeArray { get; set; } = new int[] { 1970, 1, 1 };

        public Version Version => new Version(VersionString);
        public bool NeedUpdate =>
            Version > Helper.SystemHelper.CurrentVersion //检测到的版本大于当前版本
            && new Version(Settings.Default.SkipVersion) < Version;//并且没有被设置跳过

        public DateTime Time => new DateTime(TimeArray[0], TimeArray[1], TimeArray[2]);
    }
    internal class UpdateChecker : RemoteDataGetter<UpdateCheckResult>
    {
#if USE_LOCAL_API&& DEBUG
        public override UpdateCheckResult Get()
        {
            var jsonBytes = webClient.DownloadData("http://localhost:24010/api/update/");
            var json = Encoding.UTF8.GetString(jsonBytes);
            var result = (UpdateCheckResult)JsonConvert.DeserializeObject(json, typeof(UpdateCheckResult));
            return result;
        }
#else
        public override UpdateCheckResult Get()
        {
            Logger.Info(this, "Getting update info....");
            byte[] bytes = new WebClient().DownloadData(App.Current.Resources["urlApiUpdate"].ToString());
            string data = Encoding.UTF8.GetString(bytes);
            JObject j = JObject.Parse(data);
            var result = (UpdateCheckResult)JsonConvert.DeserializeObject(j.ToString(), typeof(UpdateCheckResult));
            Logger.Info(this, "update check finished " + data);
            return result;
        }
#endif
    }
}
