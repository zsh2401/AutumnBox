using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.View.Controls;
using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.DialogContent
{
    /// <summary>
    /// ContentDonate.xaml 的交互逻辑
    /// </summary>
    public partial class ContentDonate : AtmbDialogContent
    {
        public ContentDonate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Process.Start(App.Current.Resources["urlDonatePage"].ToString());
            }
            catch (Exception ex)
            {
                SLogger.Warn(this, "can not open donate page url", ex);
            }
        }
    }
}
