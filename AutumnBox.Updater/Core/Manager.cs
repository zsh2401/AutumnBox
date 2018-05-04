using AutumnBox.Updater.Core.File;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Updater.Core
{
    static class Manager
    {
        const string urlApi = "http://atmb.top/api/fupdate";
        static readonly WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
        public static UpdateInfo ParseUpdateInfo(string value)
        {
            var str = webClient.DownloadString(urlApi);
            var jObj = JObject.Parse(str);
            var uInfo = JsonConvert.DeserializeObject<UpdateInfo>(jObj.ToString());
            return uInfo;
        }
    }
}
