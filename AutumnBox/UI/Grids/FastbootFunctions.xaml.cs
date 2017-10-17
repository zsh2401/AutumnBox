using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Helper;
using AutumnBox.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutumnBox.Basic.Devices;

namespace AutumnBox.UI.Grids
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

        public void Refresh(DeviceSimpleInfo deviceSimpleInfo)
        {
            RefreshStart?.Invoke(this, new EventArgs());
            UIHelper.SetGridButtonStatus(this, deviceSimpleInfo.Status == DeviceStatus.FASTBOOT);
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
                CustomRecoveryFlasher flasher = new CustomRecoveryFlasher(new FileArgs() { files = new string[] { fileDialog.FileName } });
                var rm = App.SelectedDevice.GetRunningManger(flasher);
                rm.FuncEvents.Finished += App.OwnerWindow.FuncFinish;
                rm.FuncStart();
                UIHelper.ShowRateBox(rm);
            }
            else
            {
                return;
            }
        }

        private void ButtonMiFlash_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRelockMi_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show(App.Current.Resources["Warning"].ToString(), App.Current.Resources["msgRelockWarning"].ToString())) return;
            if (!ChoiceBox.Show(App.Current.Resources["Warning"].ToString(), App.Current.Resources["msgRelockWarningAgain"].ToString())) return;
            XiaomiBootloaderRelocker relocker = new XiaomiBootloaderRelocker();
            var rm = App.SelectedDevice.GetRunningManger(relocker);
            rm.FuncEvents.Finished += App.OwnerWindow.FuncFinish;
            rm.FuncStart();
            UIHelper.ShowRateBox(rm);
        }
    }
}
