using System.Linq;
using System.Windows.Controls;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Management;

namespace AutumnBox.GUI.UI.Panel
{
    /// <summary>
    /// PanelCurrentDevice.xaml 的交互逻辑
    /// </summary>
    public partial class PanelCurrentDevice : UserControl,IDeviceRefreshable
    {
        public PanelCurrentDevice()
        {
            InitializeComponent();
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            PoweronPanel.Set(Manager.InternalManager.Warppers.ToArray(),deviceSimpleInfo);
            RecoveryPanel.Set(Manager.InternalManager.Warppers.ToArray(), deviceSimpleInfo);
            FastbootPanel.Set(Manager.InternalManager.Warppers.ToArray(), deviceSimpleInfo);
            DevInfoPanel.Refresh(deviceSimpleInfo);
            RebootGrid.Refresh(deviceSimpleInfo);
        }

        public void Reset()
        {
            PoweronPanel.Set(Manager.InternalManager.Warppers.ToArray());
            RecoveryPanel.Set(Manager.InternalManager.Warppers.ToArray());
            FastbootPanel.Set(Manager.InternalManager.Warppers.ToArray());
            RebootGrid.Reset();
            DevInfoPanel.Reset();
        }
    }
}
