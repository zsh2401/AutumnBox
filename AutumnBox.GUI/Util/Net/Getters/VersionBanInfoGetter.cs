using AutumnBox.Logging;
using Newtonsoft.Json;
using System.IO;

namespace AutumnBox.GUI.Util.Net.Getters
{
    class VersionBanInfoGetter : JsonGetter<VersionBanInfoGetter.Result>
    {
        public override string Url => url;
        private readonly string url;
        public VersionBanInfoGetter()
        {
#if DEBUG
            var banApi = "http://localhost:24010/_api_/ban";
#else
            var banApi = App.Current.Resources["WebApiBanInfo"].ToString();
#endif
            var versionCode = Self.Version;
            url = Path.Combine(banApi, versionCode.ToString(3)).Replace('\\', '/');
        }

        [JsonObject(MemberSerialization.OptOut)]
        public class Result : IJsonSettable
        {
            [JsonProperty("banned")]
            public bool Banned { get; set; }
            [JsonProperty("reason")]
            public string Reason { get; set; }
            public string Json { get; set; }
        }
    }
}
