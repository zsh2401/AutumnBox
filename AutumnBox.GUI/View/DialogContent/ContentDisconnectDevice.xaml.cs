using AutumnBox.GUI.View.Controls;
using AutumnBox.GUI.ViewModel;

namespace AutumnBox.GUI.View.DialogContent
{
    /// <summary>
    /// ContentDisconnectDevice.xaml 的交互逻辑
    /// </summary>
    public partial class ContentDisconnectDevice : AtmbDialogContent
    {
        public ContentDisconnectDevice()
        {
            InitializeComponent();
            DataContext = new VMDisconnectDevice()
            {
                ViewCloser = () =>
                {
                    Finish();
                }
            };
        }
    }
}
