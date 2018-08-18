using AutumnBox.Basic.Device;
using AutumnBox.GUI.ViewModel;
using AutumnBox.Support.Log;
using System;
using System.Windows.Controls;
using static AutumnBox.GUI.View.Panel.PanelDevices;

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelDevices.xaml 的交互逻辑
    /// </summary>
    public partial class PanelDevices : UserControl, INotifyDeviceChanged
    {
        public interface INotifyDeviceChanged
        {
            event EventHandler<DeviceChangedEventArgs> DeviceChanged;
            event EventHandler NoDevice;
        }
        public class DeviceChangedEventArgs : EventArgs
        {
            public DeviceBasicInfo CurrentDevice { get; set; }
        }
        private VMConnectDevices vm;
        public PanelDevices()
        {
            InitializeComponent();
            vm = new VMConnectDevices();
            DataContext = vm;
        }
        public void Work()
        {
            vm.Work();
        }
        /// <summary>
        /// 呵呵哒
        /// </summary>
        public event EventHandler<DeviceChangedEventArgs> DeviceChanged;
        /// <summary>
        /// 咯咯哒
        /// </summary>
        public event EventHandler NoDevice;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((e.Source as ListBox).SelectedIndex == -1)
            {
                NoDevice?.Invoke(this, new EventArgs());
            }
            else
            {
                var args = new DeviceChangedEventArgs()
                {
                    CurrentDevice = (DeviceBasicInfo)(e.Source as ListBox).SelectedItem
                };
                DeviceChanged?.Invoke(this, args);
            }
        }

        private void MainList_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            Logger.Debug(this, "updated");
        }
    }
}
