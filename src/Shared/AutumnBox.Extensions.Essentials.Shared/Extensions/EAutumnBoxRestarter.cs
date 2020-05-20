using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    [ExtName("Reboot AutumnBox")]
    [ClassText("confirm", "Are you sure to restart AutumnBox?", "zh-CN:确定要重启吗?")]
    class EAutumnBoxRestarter : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(INotificationManager notificationManager,
            IAppManager appManager)
        {

            //notificationManager.Info(appManager.CurrentLanguageCode);
            var task = notificationManager.Ask(this.RxGetClassText("confirm"));
            task.Wait();
            if (task.Result)
            {
                appManager.RestartAppAsAdmin();
            }
        }
    }
}
