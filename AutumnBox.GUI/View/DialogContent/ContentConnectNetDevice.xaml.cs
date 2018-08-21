using AutumnBox.GUI.View.Controls;
using AutumnBox.GUI.ViewModel;
using AutumnBox.Support.Log;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

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
                    Logger.Info(this,"??");
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
