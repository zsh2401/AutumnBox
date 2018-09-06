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
namespace AutumnBox.GUI.Windows
{
    using System;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;
    using System.Diagnostics;
    using AutumnBox.Support.Log;

    /// <summary>
    /// FileSendingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FileSendingWindow : Window
    {
        //    private FilePusher filePusher;
        //    Regex rg12 = new Regex("\\ (.*?)\\%");
        //    Regex rg3 = new Regex("\\[(.*?)\\%");
        //    public FileSendingWindow(FilePusher pusher)
        //    {
        //        this.Owner = App.Current.MainWindow;
        //        this.filePusher = pusher;
        //        this.filePusher.MustTiggerAnyFinishedEvent = true;
        //        filePusher.Finished += (s, e) =>
        //        {
        //            this.Dispatcher.Invoke(() =>
        //            {
        //                this.Close();
        //            });
        //        };
        //        filePusher.OutputReceived += (s, e) =>
        //        {
        //            if (!e.IsError)
        //            {
        //                this.OutReceived(s, e.SourceArgs);
        //            }
        //            else {
        //                this.ErrorReceived(s, e.SourceArgs);
        //            }
        //        };
        //        InitializeComponent();
        //    }

        //    public void ErrorReceived(object sender, DataReceivedEventArgs e)
        //    {
        //    }

        //    public void OutReceived(object sender, DataReceivedEventArgs e)
        //    {
        //        Match m;
        //        try
        //        {
        //            m = rg12.Match(e.Data);
        //            if (!m.Success)
        //            {
        //                m = rg3.Match(e.Data);
        //            }
        //            var r = m.Result("$1");
        //            ProgressBarMain.Dispatcher.Invoke(() =>
        //            {
        //                ProgressBarMain.Value = double.Parse(r);
        //                LabelProgressMessage.Content = r.ToString() + "%";
        //            });
        //        }
        //        catch (Exception se)
        //        {
        //            Logger.Warn(this,"parse progress text failed" ,se);
        //        }
        //    }

        //    private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        //    {
        //        filePusher.ForceStop();
        //        this.Close();
        //    }

        //    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        //    {
        //        if (e.LeftButton == MouseButtonState.Pressed)
        //        {
        //            try { this.DragMove(); } catch (InvalidOperationException) { }
        //        }
        //    }

    }
}
