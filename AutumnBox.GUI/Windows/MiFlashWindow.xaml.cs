using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutumnBox.GUI.Windows
{
    public struct MiFlashWindowShowInfo
    {
        public Serial Serial { get; set; }
        public Window Owner { get; set; }
        public DevicesMonitor DevicesMonitor { get; set; }
    }
    /// <summary>
    /// MiFlashWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MiFlashWindow : Window
    {
        private MiFlasher _currentMiFlasher;
        public MiFlashWindowShowInfo ShowInfo;
        public MiFlashWindow()
        {
            InitializeComponent();
        }
        public new void ShowDialog()
        {
            ShowInfo.DevicesMonitor.DevicesChanged += DevicesMonitor_DevicesChanged;
            Owner = ShowInfo.Owner;
            TBSerial.Text = ShowInfo.Serial.ToString();
            base.ShowDialog();
        }

        private void DevicesMonitor_DevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            if (!e.DevicesList.Contains(ShowInfo.Serial))
            {
                _currentMiFlasher?.ForceStop();
                BoxHelper.ShowMessageDialog("warning", "当前正在操作的设备被拔除!");
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShowInfo.DevicesMonitor.DevicesChanged -= DevicesMonitor_DevicesChanged;
        }

        private void BtnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog floderDialog = new FolderBrowserDialog
            {
                Description = App.Current.Resources["selectingLineFlashPackageDesc"].ToString()
            };
            if (floderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TBFilePath.Text = floderDialog.SelectedPath;
                ChangeState(State.Ready);
            }
        }

        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            if (TBFilePath.Text != "")
            {
                ChangeState(State.Flashing);
            }
            else
            {
                return;
            }
        }

        private enum State
        {
            PleaseSelectFloder,
            Ready,
            Flashing,
            Successful,
            Fail,
        }
        private void ChangeState(State state)
        {
            switch (state)
            {
                case State.PleaseSelectFloder:
                    TBState.Text = App.Current.Resources["mfPlzSelectLineFlashFloder"].ToString();
                    break;
                case State.Ready:
                    TBState.Text = App.Current.Resources["mfReady"].ToString();
                    break;
                case State.Flashing:
                    TBState.Text = App.Current.Resources["mfFlashing"].ToString();
                    break;
                case State.Successful:
                    TBState.Text = App.Current.Resources["mfSuccess"].ToString();
                    break;
                case State.Fail:
                    TBState.Text = App.Current.Resources["mfFail"].ToString();
                    break;
            }
        }
    }
}
