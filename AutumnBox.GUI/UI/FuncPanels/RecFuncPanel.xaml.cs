using System.Windows;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using System.Windows.Forms;
using AutumnBox.GUI.UI.Fp;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// RecoveryFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class RecFuncPanel : FastPanelChild, IRefreshable
    {
        private DeviceBasicInfo _currentDevInfo;
        public RecFuncPanel()
        {
            InitializeComponent();
        }

        public void Refresh(DeviceBasicInfo devInfo)
        {
            this._currentDevInfo = devInfo;
            UIHelper.SetGridButtonStatus(MainGrid,
                (_currentDevInfo.State == DeviceState.Recovery || _currentDevInfo.State == DeviceState.Sideload));
        }

        public void Reset()
        {
            _currentDevInfo = new DeviceBasicInfo() { State = DeviceState.None };
            UIHelper.SetGridButtonStatus(MainGrid, false);
        }

        private void ButtonPushFileToSdcard_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "刷机包/压缩包文件(*.zip)|*.zip|镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var args = new FilePusherArgs()
                {
                    DevBasicInfo = _currentDevInfo,
                    SourceFile = fileDialog.FileName,
                };
                var pusher = new FilePusher();
                pusher.RunAsync(args);
                new FileSendingWindow(pusher).ShowDialog();
            }
        }

        private void ButtonSideload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBackupDcim_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = App.Current.Resources["selectDcimBackupFloder"].ToString()
            };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                var args = new DcimBackuperArgs()
                {
                    DevBasicInfo = _currentDevInfo,
                    TargetPath = fbd.SelectedPath
                };
                var backuper = new DcimBackuper();
                backuper.Init(args);
                new PullingWindow(backuper) { Owner = App.Current.MainWindow }.Show();
            }
        }
    }
}
