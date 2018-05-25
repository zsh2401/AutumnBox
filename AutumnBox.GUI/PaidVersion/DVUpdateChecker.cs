/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/25 23:41:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.Properties;
using AutumnBox.Support.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.PaidVersion
{
    [JsonObject(MemberSerialization.OptOut)]
    class DVUpdateInfo
    {
        [JsonProperty("version")]
        public Version Version { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        public bool NeedUpdate
        {
            get
            {
                try
                {
                    return Version > Helper.SystemHelper.CurrentVersion //检测到的版本大于当前版本
    && new Version(Settings.Default.SkipVersion) < Version;//并且没有被设置跳过
                }
                catch (Exception ex)
                {
                    Logger.Warn(this, "A exception throwed on getting NeedUpdate Property", ex);
                    return false;
                }

            }
        }
    }
    class DVUpdateChecker : RemoteDataGetter<DVUpdateInfo>
    {
        public override DVUpdateInfo Get()
        {
#if DEBUG && USE_LOCAL_API
            var responseText = webClient
                .DownloadString(
                "http://localhost:24010/api/update_dv/simple");
#else
            var responseText = webClient
                .DownloadString(
                App.Current.Resources["urlApiUpdateDv"]
                .ToString());
#endif
            return JsonConvert.DeserializeObject<DVUpdateInfo>(responseText);
        }
    }
}
