using AutumnBox.GUI.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutumnBox.GUI.UI.Grids
{
    /// <summary>
    /// AboutControl.xaml 的交互逻辑
    /// </summary>
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();
            LabelCompiledDate.Content = DebugInfo.CompiledDate.ToString("MM-dd-yyyy");
        }

        private void TextBlockGoToOS_MouseDown(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkAutumnBoxOS"].ToString());

        private void TextBlockDonate_MouseDown(object sender, MouseButtonEventArgs e) =>
            new DonateWindow().ShowDialog();
    }
}
