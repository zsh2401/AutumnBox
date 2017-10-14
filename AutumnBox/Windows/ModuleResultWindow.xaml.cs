using AutumnBox.Basic.Function;
using AutumnBox.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutumnBox.Windows
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
            TextBlockAdvise.Text = _Result.Advise;
            TextBlockMessage.Text = _Result.Message;
        }

        public static void FastShow(ExecuteResult result)
        {
            new ModuleResultWindow(result)
            {
                Owner = App.OwnerWindow,
            }.ShowDialog();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UIHelper.DragMove(this, e);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
