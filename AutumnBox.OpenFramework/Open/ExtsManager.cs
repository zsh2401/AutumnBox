/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/21 14:24:42 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using AutumnBox.OpenFramework.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 拓展管理器
    /// </summary>
    public static class ExtsManager
    {
        /// <summary>
        ///  拓展路径
        /// </summary>
        public static string ExtensionsPath => ExtensionManager.ExtensionsPath_Internal;
        /// <summary>
        /// 加载脚本
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="path"></param>
        public static void LoadScript(Context ctx, string path)
        {
            ScriptsManager.Load(ctx, path);
        }
        /// <summary>
        /// 卸载脚本
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="script"></param>
        public static void UnloadScripts(Context ctx, IExtensionScript script)
        {
            ScriptsManager.Unload(ctx, script);
        }
        /// <summary>
        /// 重载所有脚本
        /// </summary>
        /// <param name="ctx"></param>
        public static void ReloadAllScripts(Context ctx)
        {
            ScriptsManager.ReloadAll(ctx);
        }
        /// <summary>
        /// 获取所有脚本
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static IExtensionScript[] GetScripts(Context ctx)
        {
            return ScriptsManager.GetScripts(ctx);
        }
    }
}
