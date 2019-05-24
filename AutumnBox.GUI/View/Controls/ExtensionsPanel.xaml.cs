using AutumnBox.Basic.Device;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.ViewModel;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Controls
{
    public sealed partial class ExtensionsPanel : UserControl
    {
        public DeviceState TargetDeviceState { get; set; } = AutumnBoxExtension.NoMatter;
        public ExtensionsPanel()
        {
            InitializeComponent();
            this.Loaded += UserControl_Loaded;
            (DataContext as INotifyPropertyChanged).PropertyChanged += ExtensionsPanel_PropertyChanged;
        }

        private void ExtensionsPanel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Docks")
            {
                RefreshDocks();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitTitle();
            LoadVM();
        }
        private void InitTitle()
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
        }
        private void LoadVM()
        {
            (DataContext as VMExtensions).Load(TargetDeviceState);
        }
        private void RefreshDocks()
        {
            var docks = (DataContext as VMExtensions).Docks;
            DocksPanel.Children.Clear();
            foreach (var dock in docks)
            {
                DocksPanel.Children.Add(new ExtensionDock(dock));
            }
        }
    }
}
