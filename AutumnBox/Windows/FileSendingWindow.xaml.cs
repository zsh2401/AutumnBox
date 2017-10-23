/* =============================================================================*\
*
* Filename: FileSendingWindow.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 9/13/2017 01:06:09(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Windows
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;
    using System.Diagnostics;
    using AutumnBox.Helper;
    using AutumnBox.Util;
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Function;
    using AutumnBox.Basic.Function.Modules;

    /// <summary>
    /// FileSendingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FileSendingWindow : Window, IOutReceiver
    {
        private FunctionModuleProxy ModuleProxy;
        Regex rg12 = new Regex("\\ (.*?)\\%");
        Regex rg3 = new Regex("\\[(.*?)\\%");
        public FileSendingWindow(FunctionModuleProxy fmp)
        {
            this.Owner = App.OwnerWindow;
            this.ModuleProxy = fmp;
            fmp.Finished += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Close();
                });
            };
            fmp.ErrorReceived += this.ErrorReceived;
            fmp.OutReceived += this.OutReceived;
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
                App.LogD(e.ToString(), se.Message);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ModuleProxy.ForceStop();
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.LogD(this.GetType().Name, "Mouse Down on Window");
            UIHelper.DragMove(this, e);
        }

    }
}
