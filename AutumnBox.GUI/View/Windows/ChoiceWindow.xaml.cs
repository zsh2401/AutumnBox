using AutumnBox.GUI.Util.Debugging;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// ChoiceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChoiceWindow : Window, INotifyPropertyChanged
    {
        public int ClickedBtn { get; private set; } = 0;
        public const int BTN_CANCEL = 0;
        public const int BTN_LEFT = 1;
        public const int BTN_RIGHT = 2;
        public string Message
        {
            get => _message; set
            {
                _message = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string _message;

        public string BtnLeft
        {
            get => _btnLeft; set
            {
                _btnLeft = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string _btnLeft;

        public string BtnRight
        {
            get => _btnRight; set
            {
                _btnRight = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string _btnRight;

        public string BtnCancel
        {
            get => _btnCancel; set
            {
                _btnCancel = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string _btnCancel;

        public ChoiceWindow()
        {
            InitializeComponent();
            BtnCancel = "ChoiceWindowBtnCancel";
            BtnLeft = "ChoiceWindowBtnNo";
            BtnRight = "ChoiceWindowBtnYes";
            DataContext = this;
            this.Closed += (s, e) =>
            {
                SGLogger<ChoiceWindow>.Debug("closed" + DialogResult);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClickedBtn = BTN_CANCEL;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClickedBtn = BTN_LEFT;
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ClickedBtn = BTN_RIGHT;
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
