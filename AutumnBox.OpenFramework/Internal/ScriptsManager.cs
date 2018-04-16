/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/15 19:00:57 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Script;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutumnBox.OpenFramework.Internal
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
        /// 重载所有脚本
        /// </summary>
        /// <param name="ctx"></param>
        public static void ReloadAll(Context ctx)
        {
            ctx.PermissionCheck(ContextPermissionLevel.Mid);
            core.ReloadAll();
        }
        /// <summary>
        /// 卸载脚本
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="script"></param>
        public static void Unload(Context ctx, IExtensionScript script)
        {
            ctx.PermissionCheck(ContextPermissionLevel.Low);
            core.Unload(script);
        }
        /// <summary>
        /// 加载脚本
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="path"></param>
        public static void Load(Context ctx, string path)
        {
            ctx.PermissionCheck(ContextPermissionLevel.Low);
            core.Load(path);
        }
        /// <summary>
        /// 获取所有脚本
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static IExtensionScript[] GetScripts(Context ctx)
        {
            ctx.PermissionCheck(ContextPermissionLevel.Mid);
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
            public void ReloadAll()
            {
                Scripts.ForEach((script) =>
                {
                    script.Dispose();
                });
                Scripts.Clear();
                string[] files = Directory.GetFiles(ExtensionManager.ExtensionsPath, "*.cs");
                OpenApi.Log.Debug(this, $"Found {files.Length} .cs file");
                foreach (var file in files)
                {
                    try
                    {
                        Scripts.Add(new ABEScript(this, file));
                    }
                    catch (Exception ex)
                    {
                        OpenApi.Log.Warn(this, $"加载{file}失败\n", ex);
                    }
                }

            }
            /// <summary>
            /// 卸载脚本
            /// </summary>
            /// <param name="script"></param>
            public void Unload(IExtensionScript script)
            {
                script.Dispose();
                core.Scripts.Remove(script);
            }
            /// <summary>
            /// 加载脚本
            /// </summary>
            /// <param name="file"></param>
            public void Load(string file)
            {
                var existExt = core.Scripts.Find((s) => { return s.FilePath == file; });
                if (existExt != null) { throw new Exception("Extension already exist"); }
                core.Scripts.Add(new ABEScript(this, file));
            }
        }
    }
}
