using AutumnBox.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.Util.Net.Getters
{
    class POTDV2Getter : JsonGetter<POTDV2Getter.Result>
    {
#if DEBUG
        public override string Url => "http://localhost:24010/_api_/potdv2";
#else
        public override string Url => App.Current.Resources["WebApiPotdV2"].ToString();
#endif

        [JsonObject(MemberSerialization.OptOut)]
        public class Result
        {
            [JsonProperty("enable")]
            public bool Enable { get; set; }
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("tenable")]
            public bool TitleEnable { get; set; }
            [JsonProperty("img")]
            public string ImgUrl { get; set; }
            [JsonProperty("target")]
            public string ClickTarget { get; set; }
            public byte[] Image { get; set; }
        }
        protected override Result ParseJson(string json)
        {
            Result result = base.ParseJson(json);
            result.Image = webClient.DownloadData(result.ImgUrl);
            return result;
        }
    }
}
