/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 21:42:35 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 脚本加载器
    /// </summary>
    public class ScriptLoader
    {
        /// <summary>
        /// 加载一个脚本
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="path"></param>
        public static void Load(Context ctx, string path)
        {
            ScriptsManager.Load(ctx, path);
        }
        /// <summary>
        /// 卸载一个脚本
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="script"></param>
        public static void Unload(Context ctx,IExtensionScript script)
        {
            ScriptsManager.Unload(ctx,script);
        }
    }
}
