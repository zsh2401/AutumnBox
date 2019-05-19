using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string MsgTitle
        {
            get => _title;
            set
            {
                _title = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string _title;

        /// <summary>
        /// 信息
        /// </summary>
        public string Message
        {
            get => _message; set
            {
                _message = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string _message;

        public MessageWindow()
        {
            InitializeComponent();
            DataContext = this;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
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
