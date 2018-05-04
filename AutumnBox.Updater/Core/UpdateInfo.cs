using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core
{
    [JsonObject(MemberSerialization.OptOut)]
    class UpdateInfo
    {
        [JsonProperty("version")]
        public Version Version { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("time")]
        public DateTime PublishTime { get; set; }
        [JsonProperty("bat")]
        public string Bat { get; set; }
        [JsonProperty("updateContent")]
        public string UpdateContent { get; set; }
        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }
        public static UpdateInfo Parse(string jsonText)
        {
            var jObj = JObject.Parse(jsonText);
            var uInfo = JsonConvert.DeserializeObject<UpdateInfo>(jObj.ToString());
            return uInfo;
        }
    }
}
