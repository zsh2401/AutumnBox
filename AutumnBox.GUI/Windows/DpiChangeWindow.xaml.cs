using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.GUI.Helper;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// DpiChangeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DpiChangeWindow : Window
    {
        private int? _deviceDefaultDpi;
        FunctionModuleProxy runningFmp;
        private int _textboxInputDpi
        {
            get
            {
                return Convert.ToInt32(TextBoxInput.Text);
            }
        }
        public DpiChangeWindow()
        {
            InitializeComponent();
            TitleBar.OwnerWindow = this;
            BtnOK.IsEnabled = true;
            new Thread(() =>
            {
                int? _deviceDefaultDpi = DeviceInfoHelper.GetDpi(App.SelectedDevice);
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
               (new DpiChangerArgs(App.SelectedDevice) { Dpi = _textboxInputDpi });
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

        internal static void FastShow(Window owner = null)
        {
            var win = new DpiChangeWindow();
            if (owner != null) win.Owner = owner;
            win.ShowDialog();
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
