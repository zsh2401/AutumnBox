/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 23:35:50 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace AutumnBox.GUI.Util.Net
{
    public class PotdGetterResult
    {
        public MemoryStream ImageMemoryStream { get; internal set; }
        public PotdRemoteInfo RemoteInfo { get; internal set; }
    }
    [JsonObject(MemberSerialization.OptOut)]
    public class PotdRemoteInfo
    {
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
    internal class PotdGetter : RemoteDataGetter<PotdGetterResult>
    {

        public override PotdGetterResult Get()
        {
            var logger = new Logger<PotdGetter>();
#if USE_LOCAL_API && DEBUG
            byte[] bytes = webClient.DownloadData("http://localhost:24010/api/potd/");
#else
             byte[] bytes = webClient.DownloadData(App.Current.Resources["urlApiPotd"].ToString());
#endif
            string html = Encoding.UTF8.GetString(bytes);
            PotdRemoteInfo remoteInfo = (PotdRemoteInfo)JsonConvert.DeserializeObject(html, typeof(PotdRemoteInfo));
            byte[] imgData = webClient.DownloadData(remoteInfo.Link);
            return new PotdGetterResult()
            {
                RemoteInfo = remoteInfo,
                ImageMemoryStream = new MemoryStream(imgData)
            };
        }
    }
}
