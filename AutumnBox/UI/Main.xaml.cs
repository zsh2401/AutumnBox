using AutumnBox.Basic;
using AutumnBox.Basic.Devices;
using AutumnBox.Images.DynamicIcons;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AutumnBox
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        Core core;
        public Window1()
        {
            InitializeComponent();
            core = new Core();
            InitEvents();
            core.dl.Start();
            ChangeButtonByStatus(DeviceStatus.NO_DEVICE);
            this.Closed += new EventHandler((obj, e) =>
            {
                core.dl.Stop();
            });
        }


        private void CustomTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


        private void LabelClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.close_normal)
            };
        }

        private void LabelClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.close_selected)
            };
        }

        private void LabelMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void LabelMin_MouseEnter(object sender, MouseEventArgs e)
        {
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.min_selected)
            };
        }

        private void LabelMin_MouseLeave(object sender, MouseEventArgs e)
        {
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.min_normal)
            };
        }

        private void DevicesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.DevicesListBox.SelectedIndex != -1)
            {
                DeviceInfo info = core.GetDeviceInfo(this.DevicesListBox.SelectedItem.ToString());
                this.AndroidVersionLabel.Content = info.androidVersion;
                this.CodeLabel.Content = info.code;
                this.ModelLabel.Content = Regex.Replace(info.brand, @"[\r\n]", "") + " " + info.model;
            }
            else {
                ChangeButtonByStatus(DeviceStatus.NO_DEVICE);
            }
        }

    }
}
