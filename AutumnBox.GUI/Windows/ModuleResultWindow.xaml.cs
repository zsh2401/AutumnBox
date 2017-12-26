using AutumnBox.Basic.Function;
using AutumnBox.GUI.Helper;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// ModuleResultWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ModuleResultWindow : Window
    {
        private ExecuteResult _Result;
        ModuleResultWindow(ExecuteResult result)
        {
            _Result = result;
            InitializeComponent();
            switch (_Result.Level)
            {
                case ResultLevel.Successful:
                    TextBlockTitle.Text = App.Current.Resources["Success"].ToString();
                    TextBlockTitle.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                case ResultLevel.MaybeSuccessful:
                    TextBlockTitle.Text = App.Current.Resources["MaybeSuccess"].ToString();
                    TextBlockTitle.Foreground = new SolidColorBrush(Colors.GreenYellow);
                    break;
                case ResultLevel.MaybeUnsuccessful:
                    TextBlockTitle.Text = App.Current.Resources["MaybeUnsuccess"].ToString();
                    TextBlockTitle.Foreground = new SolidColorBrush(Colors.OrangeRed);
                    break;
                case ResultLevel.Unsuccessful:
                    TextBlockTitle.Text = App.Current.Resources["Unsuccess"].ToString();
                    TextBlockTitle.Foreground = new SolidColorBrush(Colors.Red);
                    break;
            }
            TextBoxOutput.Text = _Result.OutputData.All.ToString();
            if (_Result.Advise == null)
            {
                TextBlockAdvise.Visibility = Visibility.Hidden;
                LabelAdviseTip.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlockAdvise.Text = _Result.Advise;
            }
            TextBlockMessage.Text = _Result.Message??"Have a nice day!";
        }

        public static void FastShow(ExecuteResult result)
        {
            new ModuleResultWindow(result)
            {
                Owner = App.Current.MainWindow,
            }.ShowDialog();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try { this.DragMove(); } catch (InvalidOperationException) { }
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TextBoxOutput.Text);
        }
    }
}
