using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.UI.CstPanels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// DevicesPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DevicesPanel : UserControl
    {
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
        private DeviceBasicInfo _currentSelectDeviceInfo
        {
            get
            {
                try
                {
                    return (DeviceBasicInfo)ListBoxMain.SelectedItem;
                }
                catch (NullReferenceException)
                {
                    return new DeviceBasicInfo() { State = DeviceState.None };
                }
            }
        }
        public event EventHandler SelectionChanged;
        public DevicesPanel()
        {
            InitializeComponent();
            BtnEnableDisableNetDebugging.Visibility = Visibility.Hidden;
            DevicesMonitor.DevicesChanged += _monitor_DevicesChanged;
        }
        private void _monitor_DevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            this.Dispatcher.Invoke(()=> {
                this.ListBoxMain.ItemsSource = e.DevicesList;
                if (ListBoxMain.SelectedIndex == -1 && e.DevicesList.Count > 0)
                {
                    ListBoxMain.SelectedIndex = 0;
                }
            });
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
            if (ListBoxMain.SelectedIndex > -1 && _currentSelectDeviceInfo.State == DeviceState.Poweron)
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

        private void BtnEnableDisableNetDebugging_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSelectionIsNetDebugging)
            {
                new FastGrid(this.GridMain, new CloseNetDebugging(_currentSelectDeviceInfo.Serial));
            }
            else
            {
                new FastGrid(this.GridMain, new OpenNetDebugging(_currentSelectDeviceInfo.Serial));
            }
        }

        private void BtnAddNetDebuggingDevice_Click(object sender, RoutedEventArgs e)
        {
            new FastGrid(this.GridMain, new NetDebuggingAdder());
        }
    }
}
