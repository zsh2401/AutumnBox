using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Function;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;

namespace AutumnBox.GUI.UI.Grids
{
    /// <summary>
    /// RecoveryFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class RecoveryFunctions : Grid, IDeviceInfoRefreshable
    {
        public RecoveryFunctions()
        {
            InitializeComponent();
        }

        public event EventHandler RefreshStart;
        public event EventHandler RefreshFinished;

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            RefreshStart?.Invoke(this, new EventArgs());
            UIHelper.SetGridButtonStatus(this,
                (deviceSimpleInfo.Status == DeviceStatus.Recovery || deviceSimpleInfo.Status == DeviceStatus.Sideload));
            RefreshFinished?.Invoke(this, new EventArgs());
        }

        public void SetDefault()
        {
            UIHelper.SetGridButtonStatus(this, false);
        }

        private void ButtonPushFileToSdcard_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "刷机包/压缩包文件(*.zip)|*.zip|镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var fmp = FunctionModuleProxy.Create<FileSender>(new FileSenderArgs(App.SelectedDevice) { FilePath = fileDialog.FileName });
                fmp.Finished += App.OwnerWindow.FuncFinish;
                fmp.AsyncRun();
                new FileSendingWindow(fmp).ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void ButtonSideload_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
