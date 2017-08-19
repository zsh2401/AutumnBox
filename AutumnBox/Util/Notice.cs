using Newtonsoft.Json.Linq;
using System.IO;

namespace AutumnBox.Util
{
#if !DEBUG
    internal struct Notice
#else
    public struct Notice
#endif
    {
        public string content;
        public int version;
        public JObject sourceData;
    }
}
