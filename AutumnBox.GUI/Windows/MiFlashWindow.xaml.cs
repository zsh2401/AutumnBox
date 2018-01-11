using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.Basic.Flows.MiFlash;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.Helper;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.IO;
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
        private State _currentState;
        private MiFlasher _currentMiFlasher;
        public MiFlashWindowShowInfo ShowInfo;
        public MiFlashWindow()
        {
            InitializeComponent();
        }
        public new void ShowDialog()
        {

            //ShowInfo.DevicesMonitor.DevicesChanged += DevicesMonitor_DevicesChanged;
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

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog floderDialog = new FolderBrowserDialog
            {
                Description = App.Current.Resources["selectingLineFlashPackageDesc"].ToString()
            };
            if (floderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var floderInfo = new LineFlashPackageParser(floderDialog.SelectedPath)
                {
                    DescSetter = (batname) =>
                    {
                        return App.Current.Resources["mfb" + batname]?.ToString() ?? batname;
                    }
                }.Parse();
                if (floderInfo.IsRight)
                {
                    TBFilePath.Text = floderDialog.SelectedPath;
                    ChangeState(State.Ready);
                    CBFlashType.ItemsSource = floderInfo.Bats;
                    CBFlashType.SelectedIndex = 0;
                }
                else
                {
                    BoxHelper.ShowMessageDialog("warrning", "msgPlzSelectARightFloder");
                }
            }
        }

        private void StartFlash()
        {
            var args = new MiFlasherArgs()
            {
                DevBasicInfo = new DeviceBasicInfo()
                {
                    State = DeviceState.Fastboot,
                    Serial = ShowInfo.Serial,
                },
                BatFileName = ((BatInfo)CBFlashType.SelectedItem).FullPath
            };
            _currentMiFlasher = new MiFlasher();
            _currentMiFlasher.Init(args);
            _currentMiFlasher.OutputReceived += (s, _e) =>
            {
                TBOutput.AppendText(_e.Text + Environment.NewLine);
            };
            _currentMiFlasher.Finished += (s, _e) =>
            {
                ChangeState(_e.Result.ResultType == Basic.FlowFramework.ResultType.Successful ? State.Successful : State.Fail);
            };
            _currentMiFlasher.RunAsync();
            ChangeState(State.Flashing);
        }
        private void StopFlashing()
        {
            _currentMiFlasher?.ForceStop();
        }
        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            if (_currentState == State.Flashing)
            {
                StopFlashing();
            }
            else
            {
                StartFlash();
            }
        }

        private void ChangeState(State state)
        {
            switch (state)
            {
                case State.PleaseSelectFloder:
                    TBState.Text = App.Current.Resources["mfPlzSelectLineFlashFloder"].ToString();
                    BtnMain.IsEnabled = false;
                    break;
                case State.Ready:
                    TBState.Text = App.Current.Resources["mfReady"].ToString();
                    BtnMain.Content = App.Current.Resources["btnStartMiFlash"];
                    BtnMain.IsEnabled = true;
                    CBFlashType.IsEnabled = true;
                    break;
                case State.Flashing:
                    TBState.Text = App.Current.Resources["mfFlashing"].ToString();
                    BtnMain.Content = App.Current.Resources["btnStopMiFlash"];
                    break;
                case State.Successful:
                    TBState.Text = App.Current.Resources["mfSuccess"].ToString();
                    break;
                case State.Fail:
                    TBState.Text = App.Current.Resources["mfFail"].ToString();
                    break;
            }
            _currentState = state;
        }

        private enum State
        {
            PleaseSelectFloder,
            Ready,
            Flashing,
            Successful,
            Fail,
        }
    }
}
