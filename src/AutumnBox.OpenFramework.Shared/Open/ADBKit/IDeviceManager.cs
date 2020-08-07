using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using System;

namespace AutumnBox.OpenFramework.Open.ADBKit
{
    /// <summary>
    /// 设备选择器
    /// </summary>
    public interface IDeviceManager
    {
        /// <summary>
        /// 获取或设置当前的选中设备
        /// </summary>
        IDevice Selected { get; set; }

        /// <summary>
        /// 获取已连接设备列表
        /// </summary>
        IDevice[] ConnectedDevices { get; }

        /// <summary>
        /// 设备拔插事件
        /// </summary>
        event DevicesChangedHandler DevicesChanged;

        /// <summary>
        /// 用户改变了选择设备时触发
        /// </summary>
        event EventHandler SelectedDeviceChanged;
    }
}
