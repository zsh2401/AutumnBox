using AutumnBox.Basic.FlowFramework.Container;
using AutumnBox.Basic.FlowFramework.States;
using AutumnBox.GUI.Helper;
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

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// FlowResultWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FlowResultWindow : Window
    {
        public FlowResultWindow(FlowResult result, string message = null, string advise = null)
        {
            InitializeComponent();
            SetTitle(result.ResultType);
            Owner = App.Current.MainWindow;
            TxtBMessage.Text = message ?? "Have a nice day!";
            TxtBOutput.Text = result.OutputData.All.ToString();
            if (advise == null)
            {
                TxtBAdvise.Visibility = Visibility.Hidden;
                TxtBAdviseTip.Visibility = Visibility.Hidden;
                Height = 330;
            }
            else {
                TxtBAdvise.Text = advise;
            }
        }
        private void SetTitle(ResultType type)
        {
            switch (type)
            {
                case ResultType.Exception:
                    TxtBTitle.Text = UIHelper.GetString("Exception");
                    TxtBTitle.Foreground = new SolidColorBrush(Colors.DarkRed);
                    break;
                case ResultType.Successful:
                    TxtBTitle.Text = UIHelper.GetString("Success");
                    TxtBTitle.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                case ResultType.Unsuccessful:
                    TxtBTitle.Text = UIHelper.GetString("Unsuccess");
                    TxtBTitle.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case ResultType.MaybeUnsuccessful:
                    TxtBTitle.Text = UIHelper.GetString("MaybeUnsuccess");
                    TxtBTitle.Foreground = new SolidColorBrush(Colors.OrangeRed);
                    break;
                case ResultType.MaybeSuccessful:
                    TxtBTitle.Text = UIHelper.GetString("MaybeSuccess");
                    TxtBTitle.Foreground = new SolidColorBrush(Colors.GreenYellow);
                    break;
            }
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UIHelper.DragMove(this, e);
        }
    }
}
