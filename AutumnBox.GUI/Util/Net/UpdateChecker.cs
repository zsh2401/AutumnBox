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
using AutumnBox.GUI.Util.Debugging;
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
        public string UpdateUrl { get; set; } = "http://www.atmb.top/";
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
                    new Logger<UpdateCheckResult>().Warn("Parse VersionString failed", ex);
                    return new Version("0.0.5");
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
                    new Logger<UpdateCheckResult>().Warn("Parse datetime failed", ex);
                    return new DateTime(1970, 1, 1);
                }
            }
        }
    }
    internal class UpdateChecker : RemoteDataGetter<UpdateCheckResult>
    {
        public override UpdateCheckResult Get()
        {
            Logger.Info("Getting update info....");
#if USE_LOCAL_API && DEBUG
            byte[] bytes = webClient.DownloadData("http://localhost:24010/api/update/");
#else
            byte[] bytes = webClient.DownloadData(App.Current.Resources["urlApiUpdate"].ToString());
#endif
            string data = Encoding.UTF8.GetString(bytes);
            var result = (UpdateCheckResult)JsonConvert.DeserializeObject(data, typeof(UpdateCheckResult));
            Logger.Info("update check finished" + Environment.NewLine + data);
            return result;
        }
    }
}
