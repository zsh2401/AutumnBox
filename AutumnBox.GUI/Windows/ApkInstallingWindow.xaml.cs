using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// ApkInstallingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ApkInstallingWindow : Window
    {
        private readonly ApkInstaller installer;
        private int _alreadyInstalledCount = 0;
        private readonly int _apkFilesCount;
        public ApkInstallingWindow(ApkInstaller installer, List<FileInfo> files)
        {
            InitializeComponent();
            this.installer = installer;
            _apkFilesCount = files.Count;
            Owner = App.Current.MainWindow;
            TBCountOfInstalled.Text = $"{_alreadyInstalledCount}/{_apkFilesCount}";
            this.installer.Finished += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Close();
                    new FlowResultWindow(e.Result).ShowDialog();
                });
            };
            this.installer.OutputReceived += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var bytes = Encoding.Default.GetBytes(e.Text);
                    TBOutput.AppendText(Encoding.UTF8.GetString(bytes) + "\n");
                    TBOutput.ScrollToEnd();
                });
            };
            this.installer.AApkIstanlltionCompleted += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    ProgressAdd();
                    if (!e.IsSuccess)//如果这次安装是失败的
                    {//询问用户是否在安装失败的情况下继续
                     //TODO   
                    }
                });
                return true;
            };
        }
        private void ProgressAdd()
        {
            _alreadyInstalledCount++;
            TBCountOfInstalled.Text = $"{_alreadyInstalledCount}/{_apkFilesCount}";
        }
        private bool ShowChoiceLabelForContinueOnError()
        {
            return true;
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            installer.ForceStop();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            installer.RunAsync();
            new TBLoadingEffect(TBLoading).Start();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try { this.DragMove(); } catch (InvalidOperationException) { }
            }
        }
    }
}
