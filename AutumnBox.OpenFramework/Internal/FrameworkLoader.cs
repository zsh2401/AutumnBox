/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/18 22:28:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Internal
{
    /// <summary>
    /// 框架加载器
    /// </summary>
    public static class FrameworkLoader
    {
        /// <summary>
        /// 初始化调试API(权限要求:中)
        /// </summary>
        /// <param name="ctx">设置者</param>
        /// <param name="apiImpl">API实现</param>
        public static void SetLogApi(Context ctx, ILogApi apiImpl)
        {
            ctx.PermissionCheck(ContextPermissionLevel.Mid);
            OpenApi.Log = apiImpl;
        }
        /// <summary>
        /// 初始化图形API(权限要求:中)
        /// </summary>
        /// <param name="ctx">设置者</param>
        /// <param name="apiImpl">API实现</param>
        public static void SetGuiApi(Context ctx, IGuiApi apiImpl)
        {
            ctx.PermissionCheck(ContextPermissionLevel.Mid);
            OpenApi.Gui = apiImpl;
        }
    }
}
