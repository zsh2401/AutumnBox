using AutumnBox.GUI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Net.Getters
{
    class TipsGetter : JsonGetter<TipsGetter.Result>
    {
        [JsonObject(MemberSerialization.OptOut)]
        public class Result : IJsonSettable
        {
            [JsonProperty("tips")]
            public List<Tip> Tips { get; set; }
            public string Json { get; set; }
        }
#if DEBUG
        public override string Url => "http://localhost:24010/_api_/tips/v1.json";
#else
        public override string Url => App.Current.Resources["WebApiTips"].ToString();
#endif
    }
}
