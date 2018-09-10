using AutumnBox.GUI.ViewModel;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.DialogContent
{
    /// <summary>
    /// ContentSettings.xaml 的交互逻辑
    /// </summary>
    public partial class ContentSettings : UserControl
    {
        public ContentSettings()
        {
            InitializeComponent();
            DataContext = new VMSettingsDialog();
        }
    }
}
