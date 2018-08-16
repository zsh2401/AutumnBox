/* =============================================================================*\
*
* Filename: MOTDGetter.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 00:58:16(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using Newtonsoft.Json;

namespace AutumnBox.GUI.Util.Net
{
    [JsonObject(MemberSerialization.OptOut)]
    internal class MOTDResult
    {
        [JsonProperty("header")]
        public string Header { get; set; }
        [JsonProperty("separator")]
        public string Separator { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
    internal class MOTDGetter : RemoteDataGetter<MOTDResult>
    {
        public override MOTDResult Get()
        {
#if USE_LOCAL_API && DEBUG
            var json = webClient.DownloadString("http://localhost:24010/api/motd/");
#else
            var json = webClient.DownloadString(App.Current.Resources["urlApiMotd"].ToString());
#endif
            var result = (MOTDResult)JsonConvert.DeserializeObject(json, typeof(MOTDResult));
            return result;
        }
    }
}
