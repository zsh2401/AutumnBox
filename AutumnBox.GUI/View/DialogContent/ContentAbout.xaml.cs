using AutumnBox.GUI.Helper;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.DialogContent
{
    /// <summary>
    /// ContentAbout.xaml 的交互逻辑
    /// </summary>
    public partial class ContentAbout : UserControl
    {
        public ContentAbout()
        {
            InitializeComponent();
            LabelVersion.Content = SystemHelper.CurrentVersion.ToString();
#if !DEBUG
            LabelVersion.Content += "-release";
#else
            LabelVersion.Content += "-debug";
#endif
            LabelCompiledDate.Content = SystemHelper.CompiledDate.ToString("MM-dd-yyyy");
        }
    }
}
