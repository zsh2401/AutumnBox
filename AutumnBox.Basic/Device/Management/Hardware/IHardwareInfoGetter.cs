/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:44:45 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Management.Hardware
{
    /// <summary>
    /// 硬件信息获取器
    /// </summary>
    public interface IHardwareInfoGetter
    {
        /// <summary>
        /// 获取剩余电量
        /// </summary>
        /// <returns></returns>
        int? GetBatteryLevel();
        /// <summary>
        /// 获取设备默认Dpi
        /// </summary>
        /// <returns></returns>
        int? GetDpi();
        /// <summary>
        /// 获取设备RAM大小
        /// </summary>
        /// <returns></returns>
        double? SizeofRam();
        /// <summary>
        /// 获取设备ROM大小
        /// </summary>
        /// <returns></returns>
        double? SizeofRom();
        /// <summary>
        /// 获取设备SOC信息
        /// </summary>
        /// <returns></returns>
        string GetSocInfo();
        /// <summary>
        /// 获取设备屏幕信息
        /// </summary>
        /// <returns></returns>
        string GetScreenInfo();
        /// <summary>
        /// 获取设备闪存信息
        /// </summary>
        /// <returns></returns>
        string GetFlashMemoryType();
    }
}
