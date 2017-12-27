using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.GUI.Helper;
using AutumnBox.Support.CstmDebug;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// MiFlash.xaml 的交互逻辑
    /// </summary>
    public partial class MiFlash : Window
    {
        private class Timer
        {
            private Thread thread;
            private System.Windows.Controls.Label timeLabel;
            public void Start(System.Windows.Controls.Label timeLabel)
            {
                this.timeLabel = timeLabel;
                thread = new Thread(_Main);
                thread.Start();
            }
            private void _Main()
            {
                int sec = 0;
                while (true)
                {
                    Thread.Sleep(1000);
                    sec++;
                    timeLabel.Dispatcher.Invoke(() =>
                    {
                        timeLabel.Content = $"{sec} S";
                    });
                }
            }
            public void Stop()
            {
                thread.Abort();

            }
        }
        private Timer timer = new Timer();
        private FunctionModuleProxy Fmp = null;
        public MiFlash()
        {
            InitializeComponent();
        }
        private void buttonStartOrCancel_Click(object sender, RoutedEventArgs e)
        {
            if (buttonStartOrCancel.Content == App.Current.Resources["btnCancel"])
            {
                Stop();
                buttonStartOrCancel.Content = App.Current.Resources["btnStart"];
                return;
            }
            else
            {
                if (Start())
                {
                    buttonStartOrCancel.Content = App.Current.Resources["btnCancel"];
                }
            }
        }
        private bool Start()
        {
            //if (TextBoxPath.Text == App.Current.Resources["PleaseSelectFloderPackageWireFlash"].ToString())
            //{
            //    BoxHelper.ShowMessageDialog("Warning", "PleaseSelectFloderPackageWireFlash");
            //    return false;
            //}
            //Fmp = FunctionModuleProxy.Create(typeof(Basic.Function.Modules.MiFlash),
            //new MiFlasherArgs(App.StaticProperty.DeviceConnection.DevInfo)
            //{
            //    FloderPath = TextBoxPath.Text,
            //    Type = GetFlashType(),
            //});
            //Fmp.OutputReceived += (s, _e) =>
            //{
            //    this.Dispatcher.Invoke(() =>
            //    {
            //        TextBoxOutput.AppendText("\n" + _e.Text);
            //        LabelNow.Foreground = _e.IsError ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
            //        LabelNow.Content = _e.Text;
            //        TextBoxOutput.ScrollToEnd();
            //    });
            //};
            //Fmp.Finished += (s, e) =>
            //{
            //    this.Dispatcher.Invoke(() =>
            //    {
            //        ModuleResultWindow.FastShow(e.Result);
            //    });
            //};
            //Fmp.AsyncRun();
            //timer.Start(this.LabelNow);
            return true;
        }
        private void Stop()
        {
            Fmp.ForceStop();
            timer.Stop();
        }
        private MiFlashType GetFlashType()
        {
            if (RBFlashAllExceptStorage.IsChecked == true) return MiFlashType.FlashAllExceptStorage;
            else if (RBFlashAllExceptStorageData.IsChecked == true) return MiFlashType.FlashAllExceptStorageAndData;
            else return MiFlashType.FlashAll;
        }
        private void buttonSelectFloder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Check(fbd.SelectedPath))
                {
                    TextBoxPath.Text = fbd.SelectedPath;
                }
                else
                {
                    BoxHelper.ShowMessageDialog("Warning", "msgMiFlashFloderError");
                }
            }
        }
        private static bool Check(string path)
        {
            if (!(File.Exists($"{path}/{Basic.Function.Modules.MiFlash.flash_all_bat} "))) return false;
            if (!(File.Exists($"{path}/{Basic.Function.Modules.MiFlash.flash_all_except_storage_bat}"))) return false;
            if (!(File.Exists($"{path}/{Basic.Function.Modules.MiFlash.flash_all_except_storage_bat}"))) return false;
            return true;
        }
    }
}
