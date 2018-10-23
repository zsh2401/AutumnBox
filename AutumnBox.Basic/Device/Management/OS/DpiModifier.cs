/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 12:47:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device.Management.Hardware;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// DPI修改器
    /// </summary>
    [Obsolete("Plz use WindowManager to instead")]
    public class DpiModifier : DeviceCommander
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public DpiModifier(IDevice device) : base(device)
        {
        }
        /// <summary>
        /// 获取设备默认DPI
        /// </summary>
        /// <returns></returns>
        public int GetSourceDpi()
        {
            int? result = new DeviceHardwareInfoGetter(Device).GetDpi();
            if (result == null)
            {
                throw new Exception("get dpi failed");
            }
            return (int)result;
        }
        /// <summary>
        /// 设置DPI
        /// </summary>
        /// <param name="dpi"></param>
        public void SetDpi(int dpi)
        {
            Device.Shell($"wm density {dpi}")
                .ThrowIfExitCodeNotEqualsZero();
        }
    }
}
