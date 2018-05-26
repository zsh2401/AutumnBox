/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/25 18:53:56 (UTC +8:00)
** desc： ...
*************************************************/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core.Impl.NpdaterImpl
{
    class NpdateFile : IFile
    {
        [JsonProperty("md5")]
        public string Md5 { get; set; }
        [JsonProperty("download")]
        public string DownloadUrl { get; set; }
        [JsonProperty("local")]
        public string LocalPath { get; set; }
    }
}
