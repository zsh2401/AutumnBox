/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 2:40:39 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备对象接口
    /// </summary>
    public interface IDevice : IEquatable<IDevice>
    {
        /// <summary>
        /// 序列号
        /// </summary>
        string SerialNumber { get; }
        /// <summary>
        /// product
        /// </summary>
        string Product { get; }
        /// <summary>
        /// model
        /// </summary>
        string Model { get; }
        /// <summary>
        /// transport_id
        /// </summary>
        string TransportId { get; }
        /// <summary>
        /// 状态
        /// </summary>
        DeviceState State { get; }
        /// <summary>
        /// 检测设备是否还处于连接状态
        /// </summary>
        bool IsAlive { get; }
        /// <summary>
        /// 刷新状态
        /// </summary>
        void RefreshState();
    }
}
