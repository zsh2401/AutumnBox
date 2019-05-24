using AutumnBox.Basic.Device;
using AutumnBox.GUI.ViewModel;
using AutumnBox.OpenFramework.Extension;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Panel
{
    /// <summary>
    /// PanelExtension.xaml 的交互逻辑
    /// </summary>
    public partial class PanelExtension : UserControl
    {
        public DeviceState TargetDeviceState { get; set; } = AutumnBoxExtension.NoMatter;
        public PanelExtension()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            string resKey = $"PanelExtensionsWhenCurrentDevice{TargetDeviceState.ToString()}";
            if (TryFindResource(resKey) != null)
            {
                TBTitle.SetResourceReference(TextBlock.TextProperty, resKey);
            }
            else
            {
                TBTitle.SetResourceReference(TextBlock.TextProperty, "PanelExtensionsWhenCurrentDeviceNoMatter");
            }
            (DataContext as VMExtensions)?.Load(TargetDeviceState);
        }
    }
}
