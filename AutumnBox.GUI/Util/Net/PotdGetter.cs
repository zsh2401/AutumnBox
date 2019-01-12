/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 23:35:50 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using Newtonsoft.Json;
using System.IO;

namespace AutumnBox.GUI.Util.Net
{
    internal class PotdGetter : JsonGetter<PotdGetter.Result>
    {
#if USE_LOCAL_API
        public override string Url => "http://localhost:24010/_api_/potd/";
#else
        public override string Url=> App.Current.Resources["WebApiPotd"].ToString();
#endif

        [JsonObject(MemberSerialization.OptOut)]
        public class Result
        {
            public MemoryStream ImageMemoryStream { get; set; }

            [JsonProperty("link")]
            public string ImageSource { get; set; }
            [JsonProperty("click")]
            public string ClickUrl { get; set; }
            [JsonProperty("enable")]
            public bool? Enable { get; set; } 
        }

        protected override Result ParseJson(string json)
        {
            Result result = base.ParseJson(json);
            byte[] imgData = webClient.DownloadData(result.ImageSource);
            SLogger.Info(this, "Converting");
            result.ImageMemoryStream = new MemoryStream(imgData);
            SLogger.Info(this, "Converted");
            return result;
        }
    }
}
