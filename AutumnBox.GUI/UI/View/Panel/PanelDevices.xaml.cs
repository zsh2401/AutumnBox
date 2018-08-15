using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.UI.ViewModel.Panel;
using AutumnBox.Support.Log;
using System;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI.View.Panel
{
    /// <summary>
    /// PanelDevices.xaml 的交互逻辑
    /// </summary>
    public partial class PanelDevices : UserControl, INotifyDeviceChanged
    {
        private VMConnectDevices vm;
        public PanelDevices()
        {
            InitializeComponent();
            vm = new VMConnectDevices();
            //vm.Model.PropertyChanged += (s, e) =>
            //{
            //    Logger.Debug(this, "WOCAOO!!!");
            //    if (e.PropertyName == "Devices")
            //    {
            //        Logger.Debug(this,"WOCAOO!!!");
            //        MainList.ItemsSource = vm.Model.Devices;
            //    }
            //};
            DataContext = vm;
        }
        public void Work()
        {
            vm.Model.StartListening();
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
            if (MainList.SelectedIndex == -1)
            {
                NoDevice?.Invoke(this, new EventArgs());
            }
            else
            {
                var args = new DeviceChangedEventArgs()
                {
                    CurrentDevice = (DeviceBasicInfo)MainList.SelectedItem
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
