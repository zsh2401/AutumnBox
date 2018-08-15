using AutumnBox.GUI.UI.ViewModel.Panel;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI.View.Panel
{
    /// <summary>
    /// PanelHome.xaml 的交互逻辑
    /// </summary>
    public partial class PanelHome : UserControl
    {
        public PanelHome()
        {
            InitializeComponent();
            DataContext = new VMHome();
        }
    }
}
