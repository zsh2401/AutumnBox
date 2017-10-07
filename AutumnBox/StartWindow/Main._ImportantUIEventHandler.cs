/* =============================================================================*\
*
* Filename: Main._ImportantUIEventHandler.cs
* Description: 
*
* Version: 1.0
* Created: 10/6/2017 03:31:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox
{
    using AutumnBox.Basic.Devices;
    using AutumnBox.Helper;
    using AutumnBox.Util;
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
                App.SelectedDevice = ((DeviceSimpleInfo)DevicesListBox.SelectedItem);
                RefreshUI();
            }
            else if (this.DevicesListBox.SelectedIndex == -1) {
                App.SelectedDevice = new DeviceSimpleInfo() { Status = DeviceStatus.NO_DEVICE };
                RefreshUI();
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
            Logger.D(this,"Change to" + TabFunctions.SelectedIndex.ToString());
        }
    }
}
