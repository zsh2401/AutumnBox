using AutumnBox.GUI.ViewModel;
using AutumnBox.OpenFramework.Wrapper;
using System.Windows.Controls;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace AutumnBox.GUI.View.Controls
{
    public sealed partial class OldExtensionSlice : UserControl
    {
        public OldExtensionSlice(IExtensionWrapper wrapper)
        {
            this.InitializeComponent();
            (this.DataContext as VMOldExtension).Wrapper = wrapper;
        }
    }
}
