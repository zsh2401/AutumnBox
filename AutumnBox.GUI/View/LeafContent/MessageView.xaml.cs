using System.Windows.Controls;

namespace AutumnBox.GUI.View.LeafContent
{
    /// <summary>
    /// MessageView.xaml 的交互逻辑
    /// </summary>
    public partial class MessageView : UserControl
    {
        public MessageView(string message)
        {
            InitializeComponent();
            TBContent.Text = message;
        }
    }
}
