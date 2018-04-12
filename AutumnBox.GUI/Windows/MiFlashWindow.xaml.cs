using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows.MiFlash;
using AutumnBox.GUI.Helper;
using AutumnBox.Support.Log;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AutumnBox.GUI.Windows
{
    public struct MiFlashWindowShowInfo
    {
        public DeviceSerialNumber Serial { get; set; }
        public Window Owner { get; set; }
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
            ChangeState(State.PleaseSelectFloder);
        }
        public new void ShowDialog()
        {
            Owner = ShowInfo.Owner;
            TBSerial.Text = ShowInfo.Serial.ToString();
            base.ShowDialog();
        }
        public new void Show()
        {
            Owner = ShowInfo.Owner;
            TBSerial.Text = ShowInfo.Serial.ToString();
            base.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _currentMiFlasher?.ForceStop();
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
                else if (!floderInfo.PathIsRight)
                {
                    BoxHelper.ShowMessageDialog("Warning", "msgPathError");
                }
                else
                {
                    BoxHelper.ShowMessageDialog("Warning", "msgPlzSelectARightFloder");
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
                this.Dispatcher.Invoke(() =>
                {
                    TBOutput.AppendText(_e.Text + "\n");
                    TBOutput.ScrollToEnd();
                });
            };
            _currentMiFlasher.Finished += (s, _e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    ChangeState(_e.Result.ResultType == Basic.FlowFramework.ResultType.Successful ? State.Successful : State.Fail);
                });
            };
            _currentMiFlasher.RunAsync();
            ChangeState(State.Flashing);
        }

        private void StopFlashing()
        {
            _currentMiFlasher?.ForceStop();
            ChangeState(State.Fail);
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
                    TBState.Foreground = new SolidColorBrush(FontColors.Plz);
                    TBState.Text = App.Current.Resources["mfPlzSelectLineFlashFloder"].ToString();
                    BtnSelect.IsEnabled = true;
                    TBLoading.Visibility = Visibility.Hidden;
                    break;
                case State.Ready:
                    TBLoading.Visibility = Visibility.Hidden;
                    BeginStoryboard((Storyboard)this.Resources["Step2Animation"]);
                    BtnSelect.IsEnabled = true;
                    CBFlashType.IsEnabled = true;

                    TBState.Foreground = new SolidColorBrush(FontColors.Ready);
                    BtnMain.Content = App.Current.Resources["btnStartMiFlash"];
                    TBState.Text = App.Current.Resources["mfReady"].ToString();
                    break;

                case State.Flashing:
                    BeginStoryboard((Storyboard)this.Resources["Step3Animation"]);
                    BtnSelect.IsEnabled = false;
                    CBFlashType.IsEnabled = false;
                    TBLoading.Visibility = Visibility.Visible;
                    TBOutput.Clear();

                    TBState.Foreground = new SolidColorBrush(FontColors.Flashing);
                    BtnMain.Content = App.Current.Resources["btnStopMiFlash"];
                    TBState.Text = App.Current.Resources["mfFlashing"].ToString();
                    break;
                case State.Successful:
                    TBLoading.Visibility = Visibility.Hidden;
                    BtnSelect.IsEnabled = true;
                    CBFlashType.IsEnabled = true;

                    TBState.Text = App.Current.Resources["mfSuccess"].ToString();
                    TBState.Foreground = new SolidColorBrush(FontColors.Success);
                    BtnMain.Content = App.Current.Resources["btnStartMiFlash"].ToString();
                    break;
                case State.Fail:
                    TBLoading.Visibility = Visibility.Hidden;
                    BtnSelect.IsEnabled = true;
                    CBFlashType.IsEnabled = true;

                    TBState.Text = App.Current.Resources["mfFail"].ToString();
                    BtnMain.Content = App.Current.Resources["btnStartMiFlash"].ToString();
                    TBState.Foreground = new SolidColorBrush(FontColors.Fail);
                    break;
            }
            _currentState = state;
        }


        private static class FontColors
        {
            public static readonly Color Flashing = Colors.Orange;
            public static readonly Color Fail = Colors.Red;
            public static readonly Color Ready = Colors.YellowGreen;
            public static readonly Color Success = Colors.Green;
            public static readonly Color Plz = Colors.MediumPurple;
        }
        private enum State
        {
            PleaseSelectFloder,
            Ready,
            Flashing,
            Successful,
            Fail,
        }


        private void TBDownloadLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(App.Current.Resources["urlDownloadMiFlashPackage"].ToString());
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            new TBLoadingEffect(TBLoading).Start();
            Task.Run(() =>
            {
                try
                {
                    DeviceSerialNumber serial = null;
                    this.Dispatcher.Invoke(() =>
                    {
                        serial = ShowInfo.Serial;
                    });
                    var product = new DeviceInfoGetterInFastboot(serial).GetProduct();
                    this.Dispatcher.Invoke(() =>
                    {
                        TBSerial.Text += product == null ? null : "--" + product;
                    });
                }
                catch (Exception ex)
                {
                    Logger.Warn(this, "A exception happend  when getting product info on fastboot", ex);
                }
            });
        }
    }
}
