using AutumnBox.GUI.ViewModel;
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

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// InputWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InputWindow : Window
    {
        private readonly string hint;
        public Func<string, bool> InputCheck
        {
            get
            {
                return (DataContext as VMInputWindow)?.InputCheck;
            }
            set
            {
                (DataContext as VMInputWindow).InputCheck = value;
            }
        }

        public string Hint
        {
            get
            {
                return (DataContext as VMInputWindow).Hint;
            }
            set
            {
                (DataContext as VMInputWindow).Hint = value;
            }
        }

        public string Result { get; private set; }

        public InputWindow(string hint, Func<string, bool> inputCheck = null)
        {
            InitializeComponent();
            Loaded += InputWindow_Loaded;
            this.hint = hint;
            this.InputCheck = inputCheck;
        }

        private void InputWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as VMInputWindow;
            vm.CloseAction = (result) =>
            {
                Result = result;
                DialogResult = result == null ? false : true;
            };
            vm.Hint = hint;
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
