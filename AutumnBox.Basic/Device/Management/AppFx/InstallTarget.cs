/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/29 23:50:54 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 决定包安装的位置
    /// </summary>
    public enum InstallTarget
    {
        /// <summary>
        /// Default
        /// </summary>
        Default,
        /// <summary>
        /// Install package on the internal system memory.
        /// </summary>
        InternalSystemMemory,
        /// <summary>
        /// Install package on the shared mass storage (such as sdcard).
        /// </summary>
        [Obsolete("Nowadays,install apps to sdcard will let your device slowly")]
        SharedMassStorage
    }
}
