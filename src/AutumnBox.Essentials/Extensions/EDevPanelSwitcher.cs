using AutumnBox.Essentials.XCards;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Extensions
{
    [ExtName("ExtDevPanel Switcher")]
    [ExtDeveloperMode]
    [ExtIcon("Resources.Icons.devpanelswitcher.png")]
    class EDevPanelSwitcher : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(DevPanel devPanel, IXCardsManager xCardsManager, INotificationManager notificationManager)
        {
            if (!devPanel.Enable)
            {
                xCardsManager.Register(devPanel);
                notificationManager.Info("Dev Panel Is Enabled");
                devPanel.Enable = true;
            }
            else
            {
                xCardsManager.Unregister(devPanel);
                notificationManager.Warn("Dev Panel Is Disabled");
                devPanel.Enable = false;
            }
        }
    }
}
