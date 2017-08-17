using Newtonsoft.Json.Linq;
using System.IO;

namespace AutumnBox.Util
{
    internal struct Notice
    {
        public string content;
        public int version;
        public JObject sourceData;
    }
}
