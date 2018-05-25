/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/25 18:54:41 (UTC +8:00)
** desc： ...
*************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core.Impl
{
    [JsonObject(MemberSerialization.OptOut)]
    class NpdateInfo : IUpdateInfo
    {
        [JsonProperty("version")]
        public Version Verision { get; set; }
        [JsonProperty("updateContent")]
        public string UpdateContent { get; set; }
        public IEnumerable<IFile> Files => FilesImpl;
        [JsonProperty("files")]
        public NpdateFile[] FilesImpl { get; set; }
    }
}
