using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core.File
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
        [JsonProperty("updateContent")]
        public string UpdateContent { get; set; }
        [JsonProperty("files")]
        public FileInfo[] Files { get; set; }
    }
}
