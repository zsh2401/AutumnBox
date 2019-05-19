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
    /// ExceptionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ExceptionWindow : Window, INotifyPropertyChanged
    {
        public string MessageTitle
        {
            get => _msgTitle;
            set
            {
                _msgTitle = App.Current.Resources[value] as string ?? value;
                RaisePropertyChanged();
            }
        }
        private string _msgTitle;

        public string Sketch
        {
            get => sketch;
            set
            {
                sketch = App.Current.Resources[value] as string ?? value;
                RaisePropertyChanged();
            }
        }
        private string sketch;

        public string ExceptionMessage
        {
            get => _exMsg;
            set
            {
                _exMsg = App.Current.Resources[value] as string ?? value;
                RaisePropertyChanged();
            }
        }
        private string _exMsg;

        public ExceptionWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void RaisePropertyChanged([CallerMemberName]string memeberName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memeberName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ExceptionMessage);
        }
    }
}
