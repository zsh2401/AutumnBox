using AutumnBox.GUI.ViewModels;
using System.Windows.Controls;

namespace AutumnBox.GUI.Views.Windows
{
    /// <summary>
    /// LeafWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LeafWindow 
    {
        public LeafWindow()
        {
            InitializeComponent();
            (DataContext as VMLeafUI).View = this;
        }

        private void DetailsTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).ScrollToEnd();
        }
    }
}
