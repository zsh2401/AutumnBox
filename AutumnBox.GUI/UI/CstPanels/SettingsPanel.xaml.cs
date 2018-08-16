using AutumnBox.GUI.Helper;
using AutumnBox.GUI.I18N;
using AutumnBox.GUI.Properties;
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
            CKBShowDebugWindowOnNextLaunch.IsChecked =Settings.Default.ShowDebuggingWindowNextLaunch;
            CKBNotifyOnFinished.IsChecked = Settings.Default.NotifyOnFinish;

            CbBoxLanguage.ItemsSource = LanguageHelper.Langs;
            CbBoxLanguage.SelectedIndex = LanguageHelper.FindIndex(App.Current.Resources["LanguageCode"].ToString());
            CbBoxLanguage.SelectionChanged += CbBoxLanguage_SelectionChanged;
            
            CbBoxTheme.ItemsSource = ThemeManager.Themes;
            CbBoxTheme.SelectedIndex = ThemeManager.GetCrtIndex();
            CbBoxTheme.SelectionChanged += CbBoxTheme_SelectionChanged;
        }

        private void CbBoxTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThemeManager.ChangeTheme((ITheme)CbBoxTheme.SelectedItem);
        }

        public override void OnPanelClosed()
        {
            base.OnPanelClosed();
            Settings.Default.NotifyOnFinish = CKBNotifyOnFinished.IsChecked == true;
            Settings.Default.ShowDebuggingWindowNextLaunch = CKBShowDebugWindowOnNextLaunch.IsChecked == true;
            Settings.Default.Save();
        }

        private void CbBoxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageHelper.SetLanguage((Language)CbBoxLanguage.SelectedItem);
        }

        private void BtnCreateShortcut_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SystemHelper.CreateShortcutOnDesktop("AutumnBox", System.Environment.CurrentDirectory + "/AutumnBox.GUI.exe", "The AutumnBox-Dream of us");
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new DebugWindow().Show();
        }

        public override void OnPanelHide()
        {
            base.OnPanelHide();
            Settings.Default.Save();
        }
    }
}
