using AutumnBox.GUI.Helper;
using AutumnBox.GUI.I18N;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.DialogContent
{
    /// <summary>
    /// ContentSettings.xaml 的交互逻辑
    /// </summary>
    public partial class ContentSettings : UserControl
    {
        public ContentSettings()
        {
            InitializeComponent();
            TGLaunchDebugNext.IsChecked = Settings.Default.ShowDebuggingWindowNextLaunch;

            CBLanguage.ItemsSource = LanguageHelper.Langs;
            CBLanguage.SelectedIndex = LanguageHelper.FindIndex(App.Current.Resources["LanguageCode"].ToString());
            CBLanguage.SelectionChanged += CBLanguage_SelectionChanged;
        }

        private void CBLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LanguageHelper.SetLanguage((Language)CBLanguage.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new DebugWindow().Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SystemHelper.CreateShortcutOnDesktop("AutumnBox", Environment.CurrentDirectory + "/AutumnBox.GUI.exe", "The AutumnBox-Dream of us");
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.ShowDebuggingWindowNextLaunch = TGLaunchDebugNext.IsChecked == true;
            Settings.Default.Save();
        }
    }
}
