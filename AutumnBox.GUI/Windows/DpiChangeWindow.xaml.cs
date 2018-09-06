using AutumnBox.Basic.Device;
using AutumnBox.GUI.Util.UI;
using AutumnBox.Support.Log;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// DpiChangeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DpiChangeWindow : Window
    {
        private readonly DeviceBasicInfo devinfo;
        private DpiChanger dpiChanger;
        private int _textboxInputDpi
        {
            get
            {
                return Convert.ToInt32(TextBoxInput.Text);
            }
        }
        public DpiChangeWindow(DeviceBasicInfo devinfo)
        {
            InitializeComponent();
            this.devinfo = devinfo;
            BtnOK.IsEnabled = true;
            new Thread(() =>
            {
                int? _deviceDefaultDpi = new DeviceHardwareInfoGetter(devinfo.Serial).GetDpi();
                this.Dispatcher.Invoke(() =>
                {
                    TextBlockCurrentDpi.Text = (_deviceDefaultDpi == null) ? UIHelper.GetString("GetFail") : _deviceDefaultDpi.ToString();
                    TextBoxInput.Text = (_deviceDefaultDpi == null) ? "" : _deviceDefaultDpi.ToString();
                    BtnOK.IsEnabled = true;
                });
            }).Start();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            dpiChanger = new DpiChanger();
            dpiChanger.Init(new DpiChangerArgs() {
                Dpi = _textboxInputDpi,
                DevBasicInfo = devinfo
            });
            Logger.Debug(this,"Dpi of input : " + _textboxInputDpi);
            dpiChanger.Finished += (s, _e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    BtnOK.IsEnabled = true;
                    BtnOK.Content = App.Current.Resources["btnSaveAndReboot"];
                });
            };
            BtnOK.IsEnabled = false;
            BtnOK.Content = App.Current.Resources["OnSetting"];
            dpiChanger.RunAsync();
        }

        private void TextBoxInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try { dpiChanger.ForceStop(); } catch (Exception) { }
        }
    }
}
