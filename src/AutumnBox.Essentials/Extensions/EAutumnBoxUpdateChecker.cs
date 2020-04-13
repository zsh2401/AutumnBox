using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.TextKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        [LMain]
        public void EntryPoint(INotificationManager notificationManager, IClassTextReader reader)
        {
            var dictionary = reader.Read(this);
            notificationManager.Info(dictionary[STR_KEY_CHECKING_UPDATE]);
            Thread.Sleep(5000);
            notificationManager.Warn(dictionary[STR_KEY_FAIL]);
        }
    }
}
