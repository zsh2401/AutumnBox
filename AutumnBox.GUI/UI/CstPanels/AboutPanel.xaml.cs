using AutumnBox.GUI.UI.Fp;
using AutumnBox.GUI.Util;
using System.Windows;

namespace AutumnBox.GUI.UI.CstPanels
{
    /// <summary>
    /// AboutControl.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPanel : FastPanelChild
    {
        public AboutPanel()
        {
            InitializeComponent();
            LabelVersion.Content = Self.Version.ToString();
#if! DEBUG
             LabelVersion.Content += "-release";
#else
            LabelVersion.Content += "-debug";
#endif
            LabelCompiledDate.Content = Self.CompiledDate.ToString("MM-dd-yyyy");
        }

        private void HyperLink_MouseClick(object sender, RoutedEventArgs e)
        {
            //new FastPanel(((MainWindow)App.Current.MainWindow).GridMain, new DonatePanel()).Display();
        }
    }
}
