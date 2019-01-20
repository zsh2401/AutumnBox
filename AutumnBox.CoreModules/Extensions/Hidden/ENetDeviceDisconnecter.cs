using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;

namespace AutumnBox.CoreModules.Extensions.Hidden
{
    [ExtName("Net device disconnecting", "断开网络设备连接")]
    [ExtHide]
    public class ENetDeviceDisconnecter : LeafExtensionBase
    {
        [LMain]
        private void EntryPoint()
        {

        }
    }
}
