namespace AutumnBox
{
    using AutumnBox.Basic.Devices;
    using AutumnBox.Helper;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public partial class StartWindow
    {
        /// <summary>
        /// 当设备选择列表的被选项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevicesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DevicesListBox.SelectedIndex != -1)//如果选择了设备
            {
                new Thread(() =>
                {
                    RefreshUI();
                }).Start();
                UIHelper.ShowRateBox(this);
            }
            else
            {
                this.AndroidVersionLabel.Content = Application.Current.FindResource("PleaseSelectedADevice").ToString();
                this.CodeLabel.Content = Application.Current.FindResource("PleaseSelectedADevice").ToString();
                this.ModelLabel.Content = Application.Current.FindResource("PleaseSelectedADevice").ToString();
                ChangeButtonByStatus(DeviceStatus.NO_DEVICE);
                ChangeImageByStatus(DeviceStatus.NO_DEVICE);
            }
        }

        /// <summary>
        /// 移动窗口位置,这个这个grid实际上是我自己做的标题栏罢了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UIHelper.DragMove(this, e);
        }

        /// <summary>
        /// 主界面的被选择功能选项卡发生改变时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
