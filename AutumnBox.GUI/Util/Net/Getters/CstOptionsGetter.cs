using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Net.Getters
{
    class CstOptionsGetter : JsonGetter<CstOptionsGetter.Result>
    {
#if DEBUG
        public override string Url => "http://localhost:24010/_api_/cst/v1.json";
#else
                public override string Url => App.Current.Resources["WebApiCstOpt"].ToString();
#endif
        [JsonObject(MemberSerialization.OptOut)]
        public class Result
        {
            [JsonProperty("enable")]
            public bool Enable { get; set; }
        }
    }
}
