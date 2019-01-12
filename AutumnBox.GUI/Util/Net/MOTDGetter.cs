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
        [JsonProperty("message")]
        public string Message { get; set; }
    }
    internal class MOTDGetter : JsonGetter<MOTDResult>
    {
#if USE_LOCAL_API
        public override string Url => "http://localhost:24010/_api_/motd/";
#else
        public override string Url => (string)App.Current.Resources["WebApiMotd"];
#endif

    }
}
