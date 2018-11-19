/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 19:49:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Net
{
    internal class JsonGetter<TResultObject>
    {
        protected readonly WebClient webClient;
        public string Url
        {
            get
            {
#if USE_LOCAL_API && DEBUG
                return DebugUrl ?? _url;
#else
                return _url;
#endif
            }
            set
            {
                _url = value;
            }
        }
        private string _url;
        public string DebugUrl { get; set; } = null;
        public JsonGetter(string url) : this()
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("message", nameof(url));
            }
        }
        public JsonGetter()
        {
            webClient = new WebClient()
            {
                Encoding = Encoding.UTF8
            };
        }
        public void Try(Action<TResultObject> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            Task.Run(() =>
            {
                try
                {
                    var result = ParseJson(GetJson());
                    callback(result);
                }
                catch (Exception e)
                {
                    SLogger.Warn(this, $"The operation of get and parse json from {Url} is failed", e);
                }
            });
        }
        protected virtual TResultObject ParseJson(string json)
        {
            var obj = JsonConvert.DeserializeObject<TResultObject>(json);
            return obj;
        }
        protected virtual string GetJson()
        {
            return webClient.DownloadString(Url);
        }
    }
}
