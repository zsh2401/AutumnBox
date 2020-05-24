using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;
using System.Net;
using System.Text;
using System.Threading;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    [ExtName("AutumnBox Update Chekcer")]
    [ClassText(STR_KEY_CHECKING_UPDATE, "Checking update", "zh-cn:正在检查更新")]
    [ClassText(STR_KEY_FAIL, "Can't check update", "zh-cn:更新检查失败")]

    class EAutumnBoxUpdateChecker : LeafExtensionBase
    {
        private const string STR_KEY_CHECKING_UPDATE = "checking_update";
        private const string STR_KEY_FAIL = "cant_check_update";
        private const string URL_UPDATE_CHECK_API = "";
        private const string URL_API_SHOULD_UPDATE_CHECK_FMT = "http://aback.zsh2401.top/api/v2020/latest-version";

        private readonly WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };

        [LMain]
        public void EntryPoint(INotificationManager notificationManager, ClassTextReader reader)
        {
            notificationManager.Info(reader[STR_KEY_CHECKING_UPDATE]);
            Thread.Sleep(5000);
            notificationManager.Warn(reader[STR_KEY_FAIL]);
        }
    }
}
