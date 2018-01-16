/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 23:35:50 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Support.CstmDebug;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.NetUtil
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
        public override PotdGetterResult LocalMethod()
        {
            var html = webClient.DownloadString("http://localhost:24010/api/potd/");
            var remoteInfo = (PotdRemoteInfo)JsonConvert.DeserializeObject(html, typeof(PotdRemoteInfo));
            var imgData = webClient.DownloadData(remoteInfo.Link);
            Logger.T("get finished..");
            return new PotdGetterResult()
            {
                RemoteInfo = remoteInfo,
                ImageMemoryStream = new MemoryStream(imgData)
            };
        }

        public override PotdGetterResult NetMethod()
        {
            var html = webClient.DownloadString(Urls.POTD_API);
            var remoteInfo = (PotdRemoteInfo)JsonConvert.DeserializeObject(html, typeof(PotdRemoteInfo));
            var imgData = webClient.DownloadData(remoteInfo.Link);
            return new PotdGetterResult()
            {
                RemoteInfo = remoteInfo,
                ImageMemoryStream = new MemoryStream(imgData)
            };
        }
    }
}
