/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 18:28:42 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;

namespace AutumnBox.Basic.MultipleDevices
{
    /// <summary>
    /// 当连接设备拔插时的触发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DevicesChangedHandler(object sender, DevicesChangedEventArgs e);
    /// <summary>
    /// 连接设备变化的事件的参数
    /// </summary>
    public class DevicesChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 已连接设备列表
        /// </summary>
        public IEnumerable<IDevice> Devices { get; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="devList"></param>
        public DevicesChangedEventArgs(IEnumerable<IDevice> devList)
        {
            Devices = devList;
        }
    }
}
