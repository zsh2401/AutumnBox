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
    public partial class FastbootFuncPanel : UserControl, IRefreshable
    {
        public FastbootFuncPanel()
        {
            InitializeComponent();
        }

        public void Refresh(DeviceBasicInfo deviceSimpleInfo)
        {
            UIHelper.SetGridButtonStatus(MainGrid, deviceSimpleInfo.Status == DeviceStatus.Fastboot);
        }

        public void Reset()
        {
            UIHelper.SetGridButtonStatus(MainGrid, false);
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
                fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
                fmp.AsyncRun();
                BoxHelper.ShowLoadingDialog(fmp);
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
            if (!BoxHelper.ShowChoiceDialog("Warning", "msgRelockWarning").ToBool()) return;
            if (!BoxHelper.ShowChoiceDialog("Warning", "msgRelockWarningAgain").ToBool()) return;
            var fmp = FunctionModuleProxy.Create<XiaomiBootloaderRelocker>(new ModuleArgs(App.SelectedDevice));
            fmp.Finished += ((MainWindow)App.Current.MainWindow).FuncFinish;
            fmp.AsyncRun();
            BoxHelper.ShowLoadingDialog(fmp);
        }
    }
}
