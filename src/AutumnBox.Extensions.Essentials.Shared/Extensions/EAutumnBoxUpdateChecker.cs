using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.Logging;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    [ExtName("AutumnBox Update Chekcer")]
    [ClassText(STR_KEY_CHECKING_UPDATE, "Checking update", "zh-cn:正在检查更新")]
    [ClassText(STR_KEY_FAIL, "Can't check update", "zh-cn:更新检查失败")]
    [ClassText(STR_KEY_EVERYTHING_UP_TO_DATE, "Everything's up to date", "zh-cn:已经是最新版本啦!")]
    public class EAutumnBoxUpdateChecker : LeafExtensionBase
    {
        private const string STR_KEY_CHECKING_UPDATE = "checking_update";
        private const string STR_KEY_FAIL = "cant_check_update";
        private const string STR_KEY_EVERYTHING_UP_TO_DATE = "eup2date";
        private const string URL_API = "http://www.atmb.top/client-api/moonboat/latest-version.json";

        private readonly WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };

        [LMain]
        public void EntryPoint(INotificationManager notificationManager, ClassTextReader text, IAppManager appManager)
        {
            notificationManager.Info(text.RxGet(STR_KEY_CHECKING_UPDATE));
            try
            {
                //Download&Parse remote api's infomation.
                var jsonString = webClient.DownloadString(URL_API);
                SLogger<EAutumnBoxUpdateChecker>.Info("Letest version info has been downloaded: " + jsonString);
                var latestVersionInfo = JsonSerializer.Deserialize<LatestVersionInfo>(jsonString);

                //Check if there is newer version.
                if (latestVersionInfo.Version > appManager.Version)
                {
                    //Ask user
                    notificationManager.Ask($"{latestVersionInfo.Title}\n{latestVersionInfo.Description}").ContinueWith(task =>
                    {
                        if (task.Result)
                        {
                            appManager.OpenUrl(latestVersionInfo.DownloadUrl);
                        }
                    });
                }
                else
                {
                    //Everything's up to date.
                    notificationManager.Success(text.RxGet(STR_KEY_EVERYTHING_UP_TO_DATE));
                }
            }
            catch (Exception e)
            {
                SLogger<EAutumnBoxUpdateChecker>.Warn(e);
                notificationManager.Warn(text.RxGet(STR_KEY_FAIL));
            }
        }

        class LatestVersionInfo
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }

            [JsonPropertyName("version")]
            public string VersionString
            {
                set
                {
                    this.Version = new Version(value);
                }
            }

            [JsonIgnore]
            public Version Version { get; private set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }

            [JsonPropertyName("downloadUrl")]
            public string DownloadUrl { get; set; }
        }
    }
}
