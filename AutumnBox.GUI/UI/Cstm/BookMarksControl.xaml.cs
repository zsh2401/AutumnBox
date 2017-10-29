using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// BookMarksControl.xaml 的交互逻辑
    /// </summary>
    public partial class BookMarksControl : UserControl
    {
        public BookMarksControl()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["linkLineageOS"].ToString());
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["linkMokee"].ToString());
        }

        private void TextBlock_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["linkMiFlashToolBox"].ToString());
        }

        private void TextBlock_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["linkMiLineFlashPackage"].ToString());
        }

        private void TextBlock_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["linkDownloadMIUI"].ToString());
        }

        private void TextBlock_MouseDown_6(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["linkDownloadTwrp"].ToString());
        }

        private void TextBlock_MouseDown_7(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["linkOpenGAPPS"].ToString());
        }
    }
}
