namespace AutumnBox.UI
{
    using AutumnBox.Basic.Functions.Interface;
    using AutumnBox.Basic.Functions.RunningManager;
    using System;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;
    using System.Diagnostics;
    using AutumnBox.Helper;
    using AutumnBox.Util;

    /// <summary>
    /// FileSendingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FileSendingWindow : Window,IOutReceiver
    {
        private RunningManager rm;
        Regex rg12 = new Regex("\\ (.*?)\\%");
        Regex rg3 = new Regex("\\[(.*?)\\%");
        public FileSendingWindow(RunningManager rm):this(App.OwnerWindow,rm) {}
        public FileSendingWindow(Window Owner, RunningManager rm)
        {
            this.Owner = Owner;
            this.rm = rm;
            rm.FuncEvents.OutReceiver = this;
            rm.FuncEvents.Finished += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Close();
                });
            };
            rm.FuncEvents.OutputReceived += this.OutReceived;
            InitializeComponent();
        }

        public void ErrorReceived(object sender, DataReceivedEventArgs e)
        {
        }

        public void OutReceived(object sender, DataReceivedEventArgs e)
        {
            Match m;
            try
            {
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
                Logger.D(e.ToString(), se.Message);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            rm.FuncStop();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Logger.D(this.GetType().Name, "Mouse Down on Window");
            UIHelper.DragMove(this, e);
        }

    }
}
