using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// YNWindow.xaml 的交互逻辑
    /// </summary>
    public partial class YNWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
        public string Message
        {
            get => _message; set
            {
                _message = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string _message;

        public string BtnYES
        {
            get => _btnYES; set
            {
                _btnYES = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string _btnYES;

        public string BtnNO
        {
            get => _btnNO; set
            {
                _btnNO = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string _btnNO;
        public YNWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
