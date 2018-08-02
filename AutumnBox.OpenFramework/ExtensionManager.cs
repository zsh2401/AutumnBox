/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:28:18 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Entrance;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework
{
#if ! SDK
    /// <summary>
    /// 拓展模块管理器
    /// </summary>
    public sealed class ExtensionManager : Context, IReloadable
    {
        /// <summary>
        /// 拓展模块路径
        /// </summary>
        public static string ExtensionPath
        {
            get
            {
                return "exts";
            }
        }
        /// <summary>
        /// 已加载的所有入口类
        /// </summary>
        public IEnumerable<IEntrance> Entrances { get; private set; }
        /// <summary>
        /// 已加载的所有包装器
        /// </summary>
        public IEnumerable<IExtensionWarpper> Warppers { get; private set; }
        /// <summary>
        /// 拓展模块管理器实例
        /// </summary>
        public static ExtensionManager Instance
        {
            get
            {
                var callingAssembly = Assembly.GetCallingAssembly();
                if (callingAssembly.GetName().Name == BuildInfo.AUTUMNBOX_GUI_ASSEMBLY_NAME)
                {
                    if (__instance == null)
                    {
                        __instance = new ExtensionManager();
                    }
                    return __instance;
                }
                else
                {
                    return null;
                }
            }
        }
        private static ExtensionManager __instance;
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => "ExntesionManager";
        private ExtensionManager()
        {
            Reload();
        }
        /// <summary>
        /// 重新加载所有拓展模块
        /// </summary>
        public void Reload()
        {
            //App.RunOnUIThread(() =>
            //{
            //    App.CreateDebuggingWindow().ShowDialog();
            //});
            Logger.Info("checking ext floder");
            DirectoryInfo dir = new DirectoryInfo(ExtensionPath);
            if (!dir.Exists)
            {
                dir.Create();
                return;
            }
            FileInfo[] dlls = dir.GetFiles("*.dll");
            Logger.Info($"finded {dlls.Count()} dll files");
            IEnumerable<Assembly> assemblies = LoadAssemblies(dlls);
            Entrances = GetEntrancesFrom(assemblies);
            Warppers = GetWarppersFrom(Entrances);
            Logger.Info($"Loaded {Entrances.Count()} entrances and {Warppers.Count()} warppers");
        }
        /// <summary>
        /// 加载所有程序集
        /// </summary>
        /// <param name="files">程序集文件名</param>
        /// <returns>加载完毕的程序集</returns>
        private IEnumerable<Assembly> LoadAssemblies(FileInfo[] files)
        {
            List<Assembly> result = new List<Assembly>();
            foreach (var file in files)
            {
                try
                {
                    result.Add(Assembly.LoadFrom(file.FullName));
                }
                catch (Exception ex)
                {
                    Logger.Warn($"加载拓展模块失败({file.Name})", ex);
                }
            }
            return result;
        }
        /// <summary>
        /// 获取传入的所有程序集的入口实现
        /// </summary>
        /// <returns>所有入口类</returns>
        private IEnumerable<IEntrance> GetEntrancesFrom(IEnumerable<Assembly> assemblies)
        {
            List<IEntrance> result = new List<IEntrance>();
            foreach (var ass in assemblies)
            {
                try
                {
                    result.Add(GetExtranceFrom(ass));
                }
                catch (Exception ex)
                {
                    Logger.Warn($"获取{ass.GetName().Name}的入口类时失败,该程序集无法被加载为秋之盒拓展", ex);
                }
            }
            return result;
        }
        /// <summary>
        /// 获取程序集的入口
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private IEntrance GetExtranceFrom(Assembly assembly)
        {
            Logger.Info("Getting entrance from" + assembly.GetName().Name);
            IEntrance entrance = null;
            var types = from type in assembly.GetExportedTypes()
                        where typeof(IEntrance).IsAssignableFrom(type)
                        select type;
            if (types.Count() != 0)
            {
                entrance = (IEntrance)Activator.CreateInstance(types.First());
                return entrance;
            }
            entrance = new DefaultEntrance(assembly);
            return entrance;
        }
        /// <summary>
        /// 从所有入口类中获取所有拓展模块包装器
        /// </summary>
        /// <param name="entrances"></param>
        /// <returns></returns>
        private IEnumerable<IExtensionWarpper> GetWarppersFrom(IEnumerable<IEntrance> entrances)
        {
            List<IExtensionWarpper> result = new List<IExtensionWarpper>();
            foreach (var en in entrances)
            {
                try
                {
                    result.AddRange(en.GetWarppers());
                }
                catch (Exception ex)
                {
                    Logger.Warn($"获取拓展模块封装类失败({en.Name})", ex);
                }
            }
            return result;
        }
        /// <summary>
        ///析构
        /// </summary>
        ~ExtensionManager()
        {
            foreach (var en in Entrances)
            {
                try
                {
                    en.Destory();
                }
                catch (Exception ex)
                {
                    Logger.Warn("Destoring failed", ex);
                }
            }
        }
    }
#endif
}
