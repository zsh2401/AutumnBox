using AutumnBox.GUI.Util.UI;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelLoading.xaml 的交互逻辑
    /// </summary>
    public partial class PanelLoading : UserControl
    {
        public PanelLoading()
        {
            InitializeComponent();
            TBSentence.Text = Sentences.Next();
        }
    }
}
