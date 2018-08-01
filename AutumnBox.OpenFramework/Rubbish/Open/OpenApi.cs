/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:17:33 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 可供拓展模块调用的开放API
    /// </summary>
    public static class OpenApi
    {
        /// <summary>
        /// GUI相关的API
        /// </summary>
        public static IAppGuiManager Gui { get;internal set; }

        /// <summary>
        /// 调试相关的API
        /// </summary>
        public static ILogApi Log { get; internal set; }

    }
}
