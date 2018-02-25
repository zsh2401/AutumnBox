using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Util;
using AutumnBox.OpenFramework;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// ThridPartyFunctionPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ThridPartyFunctionPanel : UserControl, IRefreshable
    {
        private DeviceBasicInfo currentDevice;
        public ThridPartyFunctionPanel()
        {
            InitializeComponent();
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            currentDevice = deviceSimpleInfo;
            GridExtendModule.ItemsSource = from mod in ExtendModuleManager.GetModules()
                                 where mod.RequiredDeviceState.HasFlag(deviceSimpleInfo.State)
                                 select mod;
        }

        public void Reset()
        {
            currentDevice = new DeviceBasicInfo()
            {
                Serial = null,
                State = DeviceState.None
            };
            GridExtendModule.ItemsSource = null;
            Refresh(currentDevice);
        }

    }
}
