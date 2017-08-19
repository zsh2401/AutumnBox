using AutumnBox.Debug;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Util
{
    /// <summary>
    /// 引导者,这里包含着有关autumnbox 的外部链接
    /// </summary>
    internal class Guider
    {
        private JObject sourceData;
        private const string TAG = "Guider";
        public bool isOk { get; private set; }
        private const string GUIDE_URL= "https://zsh2401.github.io/autumnbox/api/guide.json";
        public Guider() {
            try
            {
                sourceData = GetSourceData(GUIDE_URL);
                isOk = true;
            }
            catch (Exception e){
                Log.d(TAG,"Get guide fail");
                Log.d(TAG, e.Message);
                sourceData = JObject.Parse("{\"ok\":\"false\"}");
                isOk = false;
            }
            Log.d(TAG, "Init ok");
        }
        public JToken this[string index] {
            get {
                    return sourceData[index];
            }
        }
        private JObject GetSourceData(string url) {
            return JObject.Parse(Tools.GetHtmlCode(url));
        }
    }
}
