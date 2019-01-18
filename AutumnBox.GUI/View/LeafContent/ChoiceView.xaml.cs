using System;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.LeafContent
{
    /// <summary>
    /// ChoiceView.xaml 的交互逻辑
    /// </summary>
    public partial class ChoiceView : UserControl
    {
        public ChoiceView(string content,string btnYes,string btnNo,string btnCancel)
        {
            InitializeComponent();
            TBContent.Text = content ?? throw new ArgumentNullException(nameof(content));
            if (btnYes != null) BtnYes.Content = btnYes;
            if (btnNo != null) BtnNo.Content = btnNo;
            if (btnCancel != null) BtnCancel.Content = btnCancel;
        }
    }
}
