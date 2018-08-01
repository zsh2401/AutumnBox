/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:39:54 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块运行前检查结果
    /// </summary>
    public enum ForerunCheckResult
    {

        /// <summary>
        /// 已经准备好运行了
        /// </summary>
        Ok = 0,
        /// <summary>
        /// 出现错误
        /// </summary>
        Error = 1,
        /// <summary>
        /// 设备状态不合适
        /// </summary>
        DeviceStateNotRight
    }
}
