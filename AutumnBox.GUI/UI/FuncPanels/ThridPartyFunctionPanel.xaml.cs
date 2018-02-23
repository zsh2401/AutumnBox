using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Mods;
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
            LstBox.ItemsSource = from mod in ModManager.Mods
                                 where mod.RequiredDeviceState.HasFlag(deviceSimpleInfo.State)
                                 select mod;
        }

        public void Reset()
        {
            currentDevice = new DeviceBasicInfo()
            {
                State = DeviceState.None
            };
            LstBox.ItemsSource = null;
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (LstBox.SelectedItem as AutumnBoxMod).Run(new StartArgs()
                {
                    Device = currentDevice
                });
            }
            catch { }

        }
    }
}
