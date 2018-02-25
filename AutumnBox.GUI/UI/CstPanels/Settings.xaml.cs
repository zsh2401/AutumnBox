using AutumnBox.GUI.Cfg;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.I18N;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.GUI.Windows;
using System.Windows.Controls;
namespace AutumnBox.GUI.UI.CstPanels
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : FastPanelChild
    {
        public Settings()
        {
            InitializeComponent();
#if! ENABLE_BLUR
            TransparencySlider.IsEnabled = false;
#endif
            TransparencySlider.Value = Config.BackgroundA;
            CbBoxLanguage.ItemsSource = LanguageHelper.Langs;
            CbBoxLanguage.SelectedIndex = LanguageHelper.GetLangIndex(App.Current.Resources["LanguageName"].ToString());
            CbBoxLanguage.SelectionChanged += CbBoxLanguage_SelectionChanged;
        }

        private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UIHelper.SetOwnerTransparency((byte)TransparencySlider.Value);
            Config.BackgroundA = (byte)TransparencySlider.Value;
        }

        private void CbBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageHelper.LoadLanguage((Language)CbBoxLanguage.SelectedItem);
        } 

        private void BtnCreateShortcut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SystemHelper.CreateShortcutOnDesktop("AutumnBox", System.Environment.CurrentDirectory + "/AutumnBox.exe", "The AutumnBox-Dream of us");
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new DebugWindow() { Owner = App.Current.MainWindow}.Show();
        }
    }
}
