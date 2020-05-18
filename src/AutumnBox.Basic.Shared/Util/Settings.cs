using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 设置
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// 是否在创建命令时创建窗口
        /// </summary>
        [Obsolete("无用的东西")]
        public static bool CreateNewWindow { get; set; } = false;
    }
}
