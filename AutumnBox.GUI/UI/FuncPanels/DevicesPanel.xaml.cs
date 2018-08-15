using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.UI.CstPanels;
using AutumnBox.GUI.UI.Fp;
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
        internal IDialogFather DialogFather { get; set; }
        public DeviceBasicInfo CurrentSelectDevice
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
            BtnDisableNetDebugging.Visibility = Visibility.Hidden;
            BtnEnableNetDebugging.Visibility = Visibility.Hidden;
            DevicesMonitor.DevicesChanged += _monitor_DevicesChanged;
            ListBoxMain.ItemsSource = DevicesMonitor.CurrentDevices;
        }

        private void ShowContentAsDialog(object content)
        {
            DialogFather.ShowDialogContent(content);
            //ContentBase.Content = content;
            //Logger.Debug(this,"WTF");
            //DialogHost.Show(ContentBase);
        }
        private void _monitor_DevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.ListBoxMain.ItemsSource = e.DevicesList;
                if (ListBoxMain.SelectedIndex == -1 && e.DevicesList.Count() > 0)
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
            if (ListBoxMain.SelectedIndex > -1 && CurrentSelectDevice.State == DeviceState.Poweron)
            {
                BtnEnableNetDebugging.Visibility = CurrentSelectionIsNetDebugging ? Visibility.Hidden : Visibility.Visible;
                BtnDisableNetDebugging.Visibility = CurrentSelectionIsNetDebugging ? Visibility.Visible : Visibility.Hidden;
            }
            else
            {
                BtnDisableNetDebugging.Visibility = Visibility.Hidden;
                BtnEnableNetDebugging.Visibility = Visibility.Hidden;
            }
            this.SelectionChanged(this, new EventArgs());
        }

        private void BtnAddNetDebuggingDevice_Click(object sender, RoutedEventArgs e)
        {
            //ShowContentAsDialog(new ContentConnectNetDevice());
            new FastPanel(this.GridMain, new NetDebuggingAdder(this)).Display();
        }

        private void BtnDisableNetDebugging_Click(object sender, RoutedEventArgs e)
        {
            new FastPanel(this.GridMain, new CloseNetDebugging(this, CurrentSelectDevice.Serial)).Display();
        }

        private void BtnEnableNetDebugging_Click(object sender, RoutedEventArgs e)
        {
            new FastPanel(this.GridMain, new OpenNetDebugging(this, CurrentSelectDevice.Serial)).Display();
        }
    }
}
