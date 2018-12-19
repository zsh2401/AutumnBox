/* =============================================================================*\
*
* Filename: StartWindow.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 10/6/2017 03:31:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Diagnostics;
using System.Windows;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.ViewModel;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs _e)
        {
            //Util.UI.HelpButtonHelper.EnableHelpButton(this, () =>
            //{
            //    try
            //    {
            //        Process.Start(App.Current.Resources["urlHelp"] as string);
            //    }
            //    catch (Exception e)
            //    {
            //        SGLogger<MainWindow>.Warn("can not open help url", e);
            //    }
            //});
            (DataContext as VMMainWindow).LoadAsync(() =>
            {
                SGLogger<MainWindow>.Info("Loaded");
                Dispatcher.Invoke(() =>
                {
                    if (Settings.Default.IsFirstLaunch)
                    {
                        new DialogContent.ContentDonate().Show();
                    }
                });
            });
        }

        private void _MainWindow_Closed(object sender, EventArgs e)
        {
            foreach (Window window in App.Current.Windows)
            {
                window.Close();
            }
        }

        private void _MainWindow_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
