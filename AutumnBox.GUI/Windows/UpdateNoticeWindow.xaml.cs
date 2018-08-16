/* =============================================================================*\
*
* Filename: UpdateNoticeWindow.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 8/16/2017 00:45:45(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Net;
using AutumnBox.Support.Log;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// UpdateNoticeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateNoticeWindow : Window
    {
        readonly UpdateCheckResult result;
        internal UpdateNoticeWindow(UpdateCheckResult e)
        {
            InitializeComponent();
            Owner = App.Current.MainWindow;
            this.result = e;
            LH.Content = result.Header;
            LbPublishTime.Content = result.Time.ToString("MM/dd/yyyy");
            TextBoxContent.Text = result.Message;
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxSkip.IsChecked == true)
            {
                Settings.Default.SkipVersion = result.VersionString;
                Settings.Default.Save();
            }
            this.Close();
        }

        private void ButtonUpdateNow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.Start(result.UpdateUrl);
            }
            catch (Exception ex)
            {
                Logger.Warn(this, "Go to update url failed..", ex);
            }
            Close();
        }
    }
}
