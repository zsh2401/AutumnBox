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
        /// 获取新窗口
        /// </summary>
#if SDK
        internal
#else
        public
#endif
        static bool CreateNewWindow { get; set; } = false;
    }
}
