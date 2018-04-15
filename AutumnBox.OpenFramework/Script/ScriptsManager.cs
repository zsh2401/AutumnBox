/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 19:00:57 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Internal;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Script
{
    /// <summary>
    /// 脚本管理器
    /// </summary>
    public static class ScriptsManager
    {
        /// <summary>
        /// 核心
        /// </summary>
        private static ScriptsManagerCore core = new ScriptsManagerCore();
        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="ctx"></param>
        public static void Reload(Context ctx) {
            ctx.PermissionCheck(ContextPermissionLevel.Low);
            core.Reload();
        }
        /// <summary>
        /// 获取所有脚本
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static IExtensionScript[] GetScripts(Context ctx) {
            ctx.PermissionCheck(ContextPermissionLevel.Low);
            return core.Scripts.ToArray();
        }
        /// <summary>
        /// 脚本管理器的核心
        /// </summary>
        private class ScriptsManagerCore : Context
        {
            /// <summary>
            /// 获取所有脚本
            /// </summary>
            public List<IExtensionScript> Scripts { get; private set; }
            /// <summary>
            /// 构建脚本管理器
            /// </summary>
            public ScriptsManagerCore()
            {
                this.Scripts = new List<IExtensionScript>();
            }
            /// <summary>
            /// 重载
            /// </summary>
            public void Reload()
            {
                Scripts.Clear();
                string[] files = Directory.GetFiles(ExtensionManager.ExtensionsPath, "*.cs");
                foreach (var file in files)
                {
                    try
                    {
                        Scripts.Add(new ABEScript(this, file));
                    }
                    catch (Exception ex)
                    {
                        OpenApi.Log.Warn(this, $"加载{file}失败", ex);
                    }
                }

            }
        }
    }
}
