using AutumnBox.GUI.Services.Impl.OS;
using AutumnBox.GUI.Util.Loader;
using AutumnBox.GUI.ViewModels;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace AutumnBox.GUI.Views.Windows
{
    /// <summary>
    /// StartupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartupWindow
    {
        public StartupWindow()
        {
            InitializeComponent();
            (DataContext as INotifyPropertyChanged).PropertyChanged += DataContextPropertyChanged;
        }

        private void DataContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is VMStartup vmstartup)
            {
                if (vmstartup.Loaded)
                {
                    App.Current.MainWindow = new MainWindowV3();
                    App.Current.MainWindow.Show();
                    Close();
                }
                else if (vmstartup.Exception != null)
                {
                    try
                    {
                        ErrorMessageBox.Show(this, vmstartup.Exception);
                    }
                    catch { }
                    App.Current.Shutdown(0);
                }
            }
        }

        [ClassText("err_msg_fmt",
            "Can not load AutumnBox\nStep name: {0}\nException name: {1}\nClick 'Yes' button to copy exception message to clipboard.\nSee more details in logs",
            "zh-CN:无法加载秋之盒! \n 步骤名: {0}\n 异常名:{1} \n 点击是按钮,将完整错误信息复制到剪贴板中\n更多内容详见日志")]
        [ClassText("err_title", "Fatal Error!", "致命问题")]
        private class ErrorMessageBox
        {
            public static void Show(Window ownerWindow, AppLoadingException e)
            {
                var classTextReader = new ClassTextReader(typeof(ErrorMessageBox));//不使用缓存
                string error_message_fmt = classTextReader["err_msg_fmt"];
                string error_message = string.Format(error_message_fmt, e.StepName, e.InnerException.InnerException.GetType().Name);
                string error_title = classTextReader["err_title"];
                if (MessageBox.Show(ownerWindow,
                    error_message,
                    error_title,
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Error) == MessageBoxResult.Yes)
                {
                    try { Clipboard.SetText($"step name:{e.StepName}\nexception: {e.InnerException.InnerException}"); } catch { }
                }
            }
        }
    }
}
