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
    public partial class SettingsPanel : FastPanelChild
    {
        public SettingsPanel()
        {
            InitializeComponent();
            CKBShowDebugWindowOnNextLaunch.IsChecked = Properties.Settings.Default.ShowDebuggingWindowNextLaunch;

            CbBoxLanguage.ItemsSource = LanguageHelper.Langs;
            CbBoxLanguage.SelectedIndex = LanguageHelper.FindIndex(App.Current.Resources["LanguageCode"].ToString());
            CbBoxLanguage.SelectionChanged += CbBoxLanguage_SelectionChanged;

            CbBoxTheme.ItemsSource = ThemeHelper.Themes;
            CbBoxTheme.SelectedIndex = ThemeHelper.GetCrtIndex();
            CbBoxTheme.SelectionChanged += CbBoxTheme_SelectionChanged;
        }

        private void CbBoxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThemeHelper.ChangeTheme((Theme)CbBoxTheme.SelectedItem);
        }

        public override void OnPanelClosed()
        {
            base.OnPanelClosed();
            Properties.Settings.Default.ShowDebuggingWindowNextLaunch = CKBShowDebugWindowOnNextLaunch.IsChecked == true;
        }


        private void CbBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageHelper.SetLanguage((Language)CbBoxLanguage.SelectedItem);
        }

        private void BtnCreateShortcut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SystemHelper.CreateShortcutOnDesktop("AutumnBox", System.Environment.CurrentDirectory + "/AutumnBox.exe", "The AutumnBox-Dream of us");
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new DebugWindow().Show();
        }
        public override void OnPanelHide()
        {
            base.OnPanelHide();
            Properties.Settings.Default.Save();
        }
        bool fuck = true;
        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            if (fuck)
            {
                ThemeHelper.ChangeTheme(ThemeHelper.Themes[1]);
            }
            else
            {
                ThemeHelper.ChangeTheme(ThemeHelper.Themes[0]);
            }
            fuck = !fuck;
        }
    }
}
