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
        [JsonObject(MemberSerialization.OptOut)]
        public class Result
        {
            public MemoryStream ImageMemoryStream { get; set; }
            [JsonProperty("canbeClosed")]
            public bool? CanbeClosed { get; set; }
            [JsonProperty("link")]
            public string Link { get; set; }
            [JsonProperty("click")]
            public string ClickUrl { get; set; }
            [JsonProperty("enable")]
            public bool? Enable { get; set; }
            [JsonProperty("isAd")]
            public bool? IsAd { get; set; }
        }
        public PotdGetter()
        {
            DebugUrl = "http://localhost:24010/_api_/potd/";
            Url = App.Current.Resources["urlApiPotd"].ToString();
        }
        protected override Result ParseJson(string json)
        {
            Result result = base.ParseJson(json);
            byte[] imgData = webClient.DownloadData(result.Link);
            SLogger.Info(this, "Converting");
            result.ImageMemoryStream = new MemoryStream(imgData);
            SLogger.Info(this, "Converted");
            return result;
        }
    }
}
