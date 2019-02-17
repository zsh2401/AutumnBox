using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Net.Getters
{
    class MotdV2Getter : JsonGetter<MotdV2Getter.Result>
    {
#if DEBUG
        public override string Url => "http://localhost:24010/_api_/motdv2";
#else
        public override string Url => App.Current.Resources["WebApiMotdV2"].ToString();
#endif
        [JsonObject(MemberSerialization.OptOut)]
        public class Result
        {
            [JsonProperty("enable")]
            public bool Enable { get; set; }

            [JsonProperty("tenable")]
            public bool TitleEnable { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("content")]
            public string Content { get; set; }
        }
    }
}
