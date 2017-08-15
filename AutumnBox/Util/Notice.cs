#define TEST_GITHUB
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Util
{
    internal class Notice
    {
        private static string noticeApiUrl = "https://raw.githubusercontent.com/zsh2401/AutumnBox/master/Api/gg.json";
        internal readonly string content;
        internal readonly int version;
        private JObject sourceData;
        public Notice(string content,int version,JObject sourceData) {
            this.content = content;
            this.version = version;
            this.sourceData = sourceData;
        }
        public static Notice GetNotice() {
            JObject d = GetSourceData();
            return new Notice(d["content"].ToString(),int.Parse(d["version"].ToString()),d);
        }
        private static JObject GetSourceData() {
#if TEST_GITHUB
            return JObject.Parse(Tools.GetHtmlCode(noticeApiUrl));
#else
            return JObject.Parse(File.ReadAllText("../Api/gg.json"));
#endif
        }
    }
}
