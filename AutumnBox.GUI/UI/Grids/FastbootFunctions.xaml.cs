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
    /// FastbootFunctions.xaml 的交互逻辑
    /// </summary>
    public partial class FastbootFunctions : Grid, IDeviceInfoRefreshable
    {
        public FastbootFunctions()
        {
            InitializeComponent();
        }

        public event EventHandler RefreshStart;
        public event EventHandler RefreshFinished;

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            RefreshStart?.Invoke(this, new EventArgs());
            UIHelper.SetGridButtonStatus(this, deviceSimpleInfo.Status == DeviceStatus.Fastboot);
            RefreshFinished?.Invoke(this, new EventArgs());
        }

        public void SetDefault()
        {
            UIHelper.SetGridButtonStatus(this, false);
        }

        private void ButtonFlashCustomRecovery_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = "选择一个文件";
            fileDialog.Filter = "镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                var fmp = FunctionModuleProxy.Create<CustomRecoveryFlasher>(new FileArgs(App.SelectedDevice) { files = new string[] { fileDialog.FileName } });
                fmp.Finished += App.OwnerWindow.FuncFinish;
                fmp.AsyncRun();
                UIHelper.ShowRateBox(fmp);
            }
            else
            {
                return;
            }
        }

        private void ButtonMiFlash_Click(object sender, RoutedEventArgs e)
        {
            new Windows.MiFlash().ShowDialog();
        }

        private void ButtonRelockMi_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.FastShow(App.Current.Resources["Warning"].ToString(), App.Current.Resources["msgRelockWarning"].ToString())) return;
            if (!ChoiceBox.FastShow(App.Current.Resources["Warning"].ToString(), App.Current.Resources["msgRelockWarningAgain"].ToString())) return;
            var fmp = FunctionModuleProxy.Create<XiaomiBootloaderRelocker>(new ModuleArgs(App.SelectedDevice));
            fmp.Finished += App.OwnerWindow.FuncFinish;
            fmp.AsyncRun();
            UIHelper.ShowRateBox(fmp);
        }
    }
}
