using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Windows;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using AutumnBox.Basic.Devices;
using AutumnBox.Helper;
using AutumnBox.Basic.Function;

namespace AutumnBox.UI.Grids
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
                (deviceSimpleInfo.Status == DeviceStatus.RECOVERY || deviceSimpleInfo.Status == DeviceStatus.SIDELOAD));
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
                FileSender fs = new FileSender(new FileArgs { files = new string[] { fileDialog.FileName } });
                RunningManager rm = App.SelectedDevice.GetRunningManger(fs);
                rm.FuncEvents.Finished += App.OwnerWindow.FuncFinish;
                rm.FuncStart();
                new FileSendingWindow(rm).ShowDialog();
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
