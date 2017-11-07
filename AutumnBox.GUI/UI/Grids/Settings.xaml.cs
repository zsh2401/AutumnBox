using AutumnBox.GUI.Cfg;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.I18N;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;

namespace AutumnBox.GUI.UI.Grids
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Grid
    {
        public Settings()
        {
            InitializeComponent();
            ColorSlider.Value = Config.BackgroundA;
            cbboxLang.ItemsSource = LanguageHelper.Langs;
            cbboxLang.DisplayMemberPath = "LanguageName";
        }

        private void cbboxLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageHelper.LoadLanguage((Language)cbboxLang.SelectedItem);
        }

        private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UIHelper.SetOwnerTransparency((byte)ColorSlider.Value);
            Config.BackgroundA = (byte)ColorSlider.Value;
        }

        private void buttonStartOrCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SystemHelper.CreateShortcutOnDesktop("AutumnBox",System.Environment.CurrentDirectory + "/AutumnBox.exe","The AutumnBox-Dream of us");
        }
    }
}
