using AutumnBox.Basic.Device;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.GUI.Helper;
using AutumnBox.Support.CstmDebug;
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
        private int? _deviceDefaultDpi;
        private readonly DeviceBasicInfo devinfo;
        FunctionModuleProxy runningFmp;
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
                int? _deviceDefaultDpi = DeviceInfoHelper.GetDpi(devinfo.Serial);
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
            runningFmp =
                FunctionModuleProxy.Create<DpiChanger>
               (new DpiChangerArgs(devinfo) { Dpi = _textboxInputDpi });
            Logger.D("Dpi for input : " + _textboxInputDpi);
            runningFmp.Finished += (s, _e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    BtnOK.IsEnabled = true;
                    BtnOK.Content = App.Current.Resources["btnSaveAndReboot"];
                });
            };
            BtnOK.IsEnabled = false;
            BtnOK.Content = App.Current.Resources["OnSetting"];
            runningFmp.AsyncRun();
        }

        private void TextBoxInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try { runningFmp.ForceStop(); } catch (Exception) { }
        }
    }
}
