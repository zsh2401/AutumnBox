using AutumnBox.GUI.MVVM;
using Newtonsoft.Json;

namespace AutumnBox.GUI.Model
{
    [JsonObject(MemberSerialization.OptOut)]
    class Tip : ModelBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("target")]
        public string Target { get; set; }
    }
}
