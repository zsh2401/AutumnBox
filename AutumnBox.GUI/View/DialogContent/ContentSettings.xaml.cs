using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.OS;
using AutumnBox.GUI.View.Windows;
using AutumnBox.GUI.ViewModel;
using AutumnBox.GUI.Windows;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.DialogContent
{
    /// <summary>
    /// ContentSettings.xaml 的交互逻辑
    /// </summary>
    public partial class ContentSettings : UserControl
    {
        public ContentSettings()
        {
            InitializeComponent();
            DataContext = new VMSettingsDialog();
        }
    }
}
