using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Functions.RunningManager;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AutumnBox.UI
{
    /// <summary>
    /// RateBox.xaml 的交互逻辑
    /// </summary>
    public partial class RateBox : Window
    {
        private RunningManager rm;
        [Obsolete]
        public RateBox(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
        }
        public RateBox(Window owner, RunningManager rm)
        {
            InitializeComponent();
            this.rm = rm;
            buttonCancel.Visibility = Visibility.Visible;
            this.Owner = owner;
        }
        public new void ShowDialog()
        {
            base.ShowDialog();
        }
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        public new void Close() {
            //(img.Source as Bitmap).Dispose();
            base.Close();
        }
        private void buttonStartBrventService_Click(object sender, RoutedEventArgs e)
        {
            this.rm.FuncStop();
            this.Close();
        }
    }
}
