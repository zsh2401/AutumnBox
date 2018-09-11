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
using System.Windows;
using AutumnBox.GUI.ViewModel;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly VMMainWindow ViewModel = new VMMainWindow();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs _e)
        {
            ViewModel.LoadAsync(() =>
            {
                Logger.Info(this, "Loaded");
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
