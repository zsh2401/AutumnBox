using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.CoreModules.Extensions.Hidden
{
    [ExtName("Net device disconnecting", "zh-cn:断开网络设备连接")]
    [ExtText("OptionOD","Only disconnect", "zh-cn:仅断开")]
    [ExtText("OptionDis", "Disable net debug", "zh-cn:关闭")]
    [ExtText("ChoiceMessage", "Close net debugging of the device after disconnected?", "zh-cn:断开网络连接后,是否关闭设备的USB网络调试?")]
    [ExtHide]
    public class ENetDeviceDisconnecter : LeafExtensionBase
    {
        [LMain]
        private void EntryPoint(ILeafUI ui, IDevice device, IClassTextDictionary texts)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                bool? choice = ui.DoChoice(texts["ChoiceMessage"],texts["OptionDis"],texts["OptionsOD"]);
                if (choice == null) return;
                (device as NetDevice).Disconnect((bool)choice);
            }
        }
    }
}
