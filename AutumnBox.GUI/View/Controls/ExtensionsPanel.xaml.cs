using AutumnBox.Basic.Device;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Controls
{
    public sealed partial class ExtensionsPanel : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("TitleProperty", typeof(string), typeof(ExtensionsPanel), new PropertyMetadata(""));

        //public DeviceState TargetState
        //{
        //    get { return (DeviceState)GetValue(TargetStateProperty); }
        //    set { SetValue(TargetStateProperty, value); }
        //}

        //public static readonly DependencyProperty TargetStateProperty =
        //    DependencyProperty.Register("TargetStateProperty", typeof(DeviceState), typeof(ExtensionsPanel), new PropertyMetadata(0));

        public ExtensionsPanel()
        {
            this.InitializeComponent();
        }
    }
}
