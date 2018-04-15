/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 20:33:40 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 脚本运行参数
    /// </summary>
    public class ScriptArgs
    {
        /// <summary>
        /// 目标设备
        /// </summary>
        public DeviceBasicInfo DeviceInfo { get; set; }
        /// <summary>
        /// 上下文
        /// </summary>
        public Context Context { get; internal set; }
    }
}
