using AutumnBox.GUI.ViewModel;
using AutumnBox.OpenFramework.Wrapper;
using System.Windows;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// LoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RunningWindow : Window
    {
        internal readonly VMRunningWindow ViewModel;
        public RunningWindow(IExtensionWrapper wrapper)
        {
            InitializeComponent();
            ViewModel = new VMRunningWindow(this, wrapper);
            DataContext = ViewModel;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = ViewModel.OnWindowClosing();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
