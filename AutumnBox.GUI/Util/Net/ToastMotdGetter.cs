/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 19:45:46 (UTC +8:00)
** desc： ...
*************************************************/
using Newtonsoft.Json;

namespace AutumnBox.GUI.Util.Net
{
    internal class ToastMotdGetter : JsonGetter<ToastMotdGetter.Result>
    {
        public ToastMotdGetter()
        {
            DebugUrl = "http://localhost:24010/api/tmotd";
            Url = "http://atmb.top/api/tmotd";
        }
        public class Result
        {
            [JsonProperty("enable")]
            public bool Enable { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("message")]
            public string Message { get; set; }
        }
    }
}
