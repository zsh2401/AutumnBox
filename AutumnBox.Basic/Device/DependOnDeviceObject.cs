/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 4:46:33 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 依赖于具体设备的对象
    /// </summary>
    [Obsolete]
    public class DependOnDeviceObject
    {
        /// <summary>
        /// 设备
        /// </summary>
        protected readonly IDevice Device;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public DependOnDeviceObject(IDevice device)
        {
            this.Device = device ?? throw new ArgumentNullException(nameof(device));
        }
    }
}
