using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        public bool SnackBarActive
        {
            get => _snackBarActive; set
            {
                _snackBarActive = value;
                RaisePropertyChanged();
            }
        }
        private bool _snackBarActive;

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

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (SnackBarActive == false)
            {
                ShowSnackBar();
            }
            DragMove();
        }
        private void ShowSnackBar()
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    SnackBarActive = true;
                });
                Thread.Sleep(2000);
                Dispatcher.Invoke(() =>
                {
                    SnackBarActive = false;
                });
            });
        }
    }
}
