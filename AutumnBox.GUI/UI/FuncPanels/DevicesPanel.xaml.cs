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
                        State = DeviceState.None
                    });
                }
                return connection;
            }
        }
        public event EventHandler SelectionChanged;
        public DevicesPanel()
        {
            InitializeComponent();
            BtnEnableDisableNetDebugging.Visibility = Visibility.Hidden;
        }
        private void _monitor_DevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            this.ListBoxMain.ItemsSource = e.DevicesList;
            if (ListBoxMain.SelectedIndex == -1 && e.DevicesList.Count > 0)
            {
                ListBoxMain.SelectedIndex = 0;
            }
        }
        private bool CurrentSelectionIsNetDebugging
        {
            get
            {
                return (ListBoxMain.SelectedIndex > -1 && ((DeviceBasicInfo)ListBoxMain.SelectedItem).Serial.IsIpAdress);
            }
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxMain.SelectedIndex > -1)
            {
                this.BtnEnableDisableNetDebugging.Visibility = Visibility.Visible;
                if (CurrentSelectionIsNetDebugging)
                {
                    this.BtnEnableDisableNetDebugging.Content = App.Current.Resources["btnDisableNetdebugging"];
                }
                else
                {
                    this.BtnEnableDisableNetDebugging.Content = App.Current.Resources["btnEnableNetdebugging"];
                }
            }
            else
            {
                this.BtnEnableDisableNetDebugging.Visibility = Visibility.Hidden;
            }
            this.SelectionChanged(this, new EventArgs());
        }
    }
}
