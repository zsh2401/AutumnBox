using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core.File
{
    [JsonObject(MemberSerialization.OptOut)]
    public class FileInfo
    {
        [JsonProperty("localRelPath")]
        public string LocalPath { get; set; }
        [JsonProperty("md5")]
        public string Md5 { get; set; }
        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }
    }
}
