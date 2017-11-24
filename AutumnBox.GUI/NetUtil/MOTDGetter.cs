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
using AutumnBox.Support.CstmDebug;
using AutumnBox.Support.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
namespace AutumnBox.GUI.NetUtil
{
    [JsonObject(MemberSerialization.OptOut)]
    public class MOTDResult
    {
        [JsonProperty("header")]
        public string Header { get; set; }
        [JsonProperty("separator")]
        public string Separator { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        internal MOTDResult() { }
    }
    [LogProperty(TAG = "MOTD Getter", Show = false)]
    internal class MOTDGetter : RemoteDataGetter<MOTDResult>
    {
        public override MOTDResult LocalMethod()
        {
            JObject o = JObject.Parse(File.ReadAllText(@"E:\zsh2401.github.io\softsupport\autumnbox\motd\index.html"));
            var result = (MOTDResult)JsonConvert.DeserializeObject(o.ToString(), typeof(MOTDResult));
            Logger.D("MOTD Get from local were success!" + result.Header + " " + result.Message);
            return result;
        }

        public override MOTDResult NetMethod()
        {
            JObject o = JObject.Parse(NetHelper.GetHtmlCode(Urls.MOTD_API));
            var result = (MOTDResult)JsonConvert.DeserializeObject(o.ToString(), typeof(MOTDResult));
            Logger.D("MOTD Get from net success!" + result.Header + " " + result.Message);
            return result;
        }
    }
}
