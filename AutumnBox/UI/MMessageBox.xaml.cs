using System;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.UI
{
    /// <summary>
    /// MMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MMessageBox : Window
    {
        public MMessageBox()
        {
            InitializeComponent();
            this.Topmost = true;
        }
        public static void ShowDialog(Window owner, string title, string content) {
            owner.Dispatcher.Invoke(new Action(() => {
                MMessageBox m = new MMessageBox();
                m.textBlockContent.Text = content;
                m.labelTitle.Content = title;
                m.Owner = owner;
                m.ShowDialog();
            }));
            
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
