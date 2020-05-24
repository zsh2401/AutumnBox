using AutumnBox.GUI.ViewModels;
using AutumnBox.Logging.Management;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutumnBox.GUI.Views.Windows
{
    /// <summary>
    /// LogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
            (DataContext as VMLog).Logs.CollectionChanged += Logs_CollectionChanged;

            KeyDown += LogWindow_KeyDown;
        }

        private void Logs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SViewer.ScrollToBottom();
        }

        private void LogWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyStates == Keyboard.GetKeyStates(Key.C) && Keyboard.Modifiers == ModifierKeys.Control)
            {
                CopySelected();
            }
        }

        private void CopySelected()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ILog log in LB.SelectedItems)
            {
                sb.Append(log.ToFormatedString());
            }
            Clipboard.SetText(sb.ToString());
        }


        private void LB_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = MouseWheelEvent,
                    Source = sender
                };
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
