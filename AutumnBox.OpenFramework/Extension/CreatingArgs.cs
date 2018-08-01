/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:18:47 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 秋之盒拓展创建时参数
    /// </summary>
    public class CreatingArgs
    {
        /// <summary>
        /// 日志标签
        /// </summary>
        public string LoggingTag { get; set; }
        /// <summary>
        /// 目标设备
        /// </summary>
        public DeviceBasicInfo Deivce { get; set; }
    }
}
