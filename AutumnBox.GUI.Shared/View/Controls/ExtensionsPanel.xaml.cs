using AutumnBox.Basic.Device;
using System.Windows;
using System.Windows.Controls;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace AutumnBox.GUI.View.Controls
{
    public sealed partial class ExtensionsPanel : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("TitleProperty", typeof(string), typeof(ExtensionsPanel), new PropertyMetadata(""));

        public DeviceState TargetState
        {
            get { return (DeviceState)GetValue(TargetStateProperty); }
            set { SetValue(TargetStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetStateProperty =
            DependencyProperty.Register("TargetStateProperty", typeof(DeviceState), typeof(ExtensionsPanel), new PropertyMetadata(0));

        public ExtensionsPanel()
        {
            this.InitializeComponent();
        }
    }
}
