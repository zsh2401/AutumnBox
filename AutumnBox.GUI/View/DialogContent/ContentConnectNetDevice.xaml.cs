using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.View.Controls;
using AutumnBox.GUI.ViewModel;
using MaterialDesignThemes.Wpf;

namespace AutumnBox.GUI.View.DialogContent
{
    /// <summary>
    /// ContentConnectNetDevice.xaml 的交互逻辑
    /// </summary>
    public partial class ContentConnectNetDevice : AtmbDialogContent
    {
        public ContentConnectNetDevice()
        {
            InitializeComponent();
            DataContext = new VMContentConnectNetDevice()
            {
                OnCloseCallback = () =>
                {
                    SGLogger<ContentConnectNetDevice>.Info("??");
                    Finish();
                }
            };
        }
        protected override void OnDialogClosing(object sender, DialogClosingEventArgs e)
        {
            base.OnDialogClosing(sender, e);
            if (((VMContentConnectNetDevice)DataContext).IsRunning)
            {
                e.Cancel();
            }
        }
    }
}
