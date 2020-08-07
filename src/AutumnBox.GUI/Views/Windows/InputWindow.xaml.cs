using AutumnBox.GUI.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.Views.Windows
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
