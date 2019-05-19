using System;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.LeafContent
{
    /// <summary>
    /// YNView.xaml 的交互逻辑
    /// </summary>
    public partial class YNView : UserControl
    {
        public YNView(string content,string btnYes,string btnNo)
        {
            InitializeComponent();
            TBContent.Text = content ?? throw new ArgumentNullException(nameof(content));
            if (btnYes != null) BtnYes.Content = btnYes;
            if (btnNo != null) BtnNo.Content = btnNo;
        }
    }
}
