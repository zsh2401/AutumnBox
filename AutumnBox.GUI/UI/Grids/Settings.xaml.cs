using AutumnBox.GUI.I18N;
using System.Windows.Controls;

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
            cbboxLang.ItemsSource = LanguageHelper.Langs;
            cbboxLang.DisplayMemberPath = "LanguageName";
        }

        private void cbboxLang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageHelper.LoadLanguage((Language)cbboxLang.SelectedItem);
        }
    }
}
