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
using AutumnBox.GUI.Properties;
using AutumnBox.Support.Log;
using Newtonsoft.Json;
using System;
using System.Text;

namespace AutumnBox.GUI.Util.Net
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

        public Version Version
        {
            get
            {
                try
                {
                    return new Version(VersionString);
                }
                catch (Exception ex)
                {
                    Logger.Warn("Parse VersionString failed", ex);
                    return new Version("0.0.5");
                }
            }
        }

        public bool NeedUpdate
        {
            get
            {
                try
                {
                    return Version > Self.Version //检测到的版本大于当前版本
    && new Version(Settings.Default.SkipVersion) < Version;//并且没有被设置跳过
                }
                catch (Exception ex)
                {
                    Logger.Warn(this, "A exception throwed on getting NeedUpdate Property", ex);
                    return false;
                }

            }
        }

        public DateTime Time
        {
            get
            {
                try
                {
                    return new DateTime(TimeArray[0], TimeArray[1], TimeArray[2]);
                }
                catch (Exception ex)
                {
                    Logger.Warn(this, "Parse datetime failed", ex);
                    return new DateTime(1970, 1, 1);
                }
            }
        }
    }
    internal class UpdateChecker : RemoteDataGetter<UpdateCheckResult>
    {
        public override UpdateCheckResult Get()
        {
            Logger.Info(this, "Getting update info....");
#if USE_LOCAL_API && DEBUG
            byte[] bytes = webClient.DownloadData("http://localhost:24010/api/update/");
#else
            byte[] bytes = webClient.DownloadData(App.Current.Resources["urlApiUpdate"].ToString());
#endif
            string data = Encoding.UTF8.GetString(bytes);
            var result = (UpdateCheckResult)JsonConvert.DeserializeObject(data, typeof(UpdateCheckResult));
            Logger.Info(this, "update check finished " + data);
            return result;
        }
    }
}
