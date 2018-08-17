using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Util;
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
            LabelVersion.Content = Self.Version.ToString();
#if !DEBUG
            LabelVersion.Content += "-release";
#else
            LabelVersion.Content += "-debug";
#endif
            LabelCompiledDate.Content = Self.CompiledDate.ToString("MM-dd-yyyy");
        }
    }
}
