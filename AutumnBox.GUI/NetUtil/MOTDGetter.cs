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
using AutumnBox.Support.Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

namespace AutumnBox.GUI.NetUtil
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
        internal MOTDResult() { }
    }
    internal class MOTDGetter : RemoteDataGetter<MOTDResult>
    {
#if USE_LOCAL_API && DEBUG
        public override MOTDResult Get()
        {
            var jsonByte = webClient.DownloadData("http://localhost:24010/api/motd/");
            var json = Encoding.Default.GetString(jsonByte);
            var result = (MOTDResult)JsonConvert.DeserializeObject(json, typeof(MOTDResult));
            return result;
        }
#else
        public override MOTDResult Get()
        {
            byte[] bytes = webClient.DownloadData(App.Current.Resources["urlApiMotd"].ToString());
            string data = Encoding.Default.GetString(bytes);
            JObject o = JObject.Parse(data);
            var result = (MOTDResult)JsonConvert.DeserializeObject(o.ToString(), typeof(MOTDResult));
            Logger.Info(this,"MOTD Get from net success!" + result.Header + " " + result.Message);
            return result;
        }
#endif
    }
}
