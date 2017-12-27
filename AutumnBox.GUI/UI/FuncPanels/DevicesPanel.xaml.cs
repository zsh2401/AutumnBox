using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Devices;
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

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// DevicesPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DevicesPanel : UserControl
    {
        public DevicesMonitor Monitor
        {
            set
            {
                if (_monitor != null)
                {
                    _monitor.DevicesChanged -= _monitor_DevicesChanged;
                }
                value.DevicesChanged += _monitor_DevicesChanged;
                _monitor = value;
            }
        }
        private DevicesMonitor _monitor;
        public DeviceConnection CurrentSelect
        {
            get
            {
                var connection = new DeviceConnection();
                if (ListBoxMain.SelectedIndex != -1)
                {
                    connection.Reset((DeviceBasicInfo)ListBoxMain.SelectedItem);
                }
                else
                {
                    connection.Reset(new DeviceBasicInfo()
                    {
                        Status = DeviceStatus.None
                    });
                }
                return connection;
            }
        }
        public event EventHandler SelectionChanged;
        public DevicesPanel()
        {
            InitializeComponent();
        }
        private void _monitor_DevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            this.ListBoxMain.ItemsSource = e.DevicesList;
            if (ListBoxMain.SelectedIndex == -1 && e.DevicesList.Count > 0) {
                ListBoxMain.SelectedIndex = 0;
            }
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectionChanged(this, new EventArgs());
        }
    }
}
