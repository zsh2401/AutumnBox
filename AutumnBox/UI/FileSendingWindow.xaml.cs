using AutumnBox.Basic.Functions;
using AutumnBox.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutumnBox.UI
{
    /// <summary>
    /// FileSendingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FileSendingWindow : Window
    {
        private RunningManager rm;
        Regex rg12 = new Regex("\\ (.*?)\\%");
        Regex rg3 = new Regex("\\[(.*?)\\%");
        public FileSendingWindow(Window Owner, RunningManager rm)
        {
            this.rm = rm;
            rm.FuncEvents.Finished += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Close();
                });
            };
            rm.FuncEvents.OutputReceived += (s, e) =>
            {
                Match m;
                try
                {
                    Log.d("SendingWindow", e.Data);
                    m = rg12.Match(e.Data);
                    if (!m.Success)
                    {
                        m = rg3.Match(e.Data);
                    }
                    var r = m.Result("$1");
                    ProgressBarMain.Dispatcher.Invoke(() =>
                    {
                        ProgressBarMain.Value = double.Parse(r);
                        LabelProgressMessage.Content = r.ToString() + "%";
                    });
                }
                catch (Exception se)
                {
                    Log.d(e.ToString(), se.Message);
                }

            };
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            rm.FuncStop();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Log.d(this.GetType().Name, "Mouse Down on Window");
            UIHelper.DragMove(this, e);
        }

    }
}
