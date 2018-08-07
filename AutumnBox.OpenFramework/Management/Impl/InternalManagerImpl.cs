/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:28:18 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Warpper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.Management.Impl
{

    /// <summary>
    /// 拓展模块管理器
    /// </summary>
    internal sealed class InternalManagerImpl : Context, IInternalManager
    {
        /// <summary>
        /// 拓展模块路径
        /// </summary>
        public string ExtensionPath
        {
            get
            {
                return "..\\exts";
            }
        }
        /// <summary>
        /// 已加载的所有入口类
        /// </summary>
        public IEnumerable<ILibrarian> Librarians { get; private set; }
        /// <summary>
        /// 已加载的所有包装器
        /// </summary>
        public IEnumerable<IExtensionWarpper> Warppers
        {
            get
            {
                return GetWarppersFrom(Librarians);
            }
        }
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => "ExntesionManager";
        /// <summary>
        /// 重新加载所有拓展模块
        /// </summary>
        public void Reload()
        {
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
            Librarians = GetLibrarianFrom(assemblies);
            Logger.Info($"loaded {Librarians.Count()} entrances and {Warppers.Count()} warppers");
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
        private IEnumerable<ILibrarian> GetLibrarianFrom(IEnumerable<Assembly> assemblies)
        {
            List<ILibrarian> result = new List<ILibrarian>();
            foreach (var ass in assemblies)
            {
                try
                {
                    ILibrarian lib = GetExtranceFrom(ass);
                    if (lib.Check())
                    {
                        lib.Ready();
                        result.Add(lib);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn($"加载与检查{ass.GetName().Name}的入口类时失败,该程序集无法被加载为秋之盒拓展", ex);
                }
            }
            return result;
        }
        /// <summary>
        /// 获取程序集的入口
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private ILibrarian GetExtranceFrom(Assembly assembly)
        {
            ILibrarian librarian = null;
            var types = from type in assembly.GetExportedTypes()
                        where IsLibrarian(type)
                        select type;
            if (types.Count() != 0)
            {
                librarian = (ILibrarian)Activator.CreateInstance(types.First());
                return librarian;
            }
            librarian = new DefaultLibrarian(assembly);
            return librarian;
        }
        /// <summary>
        /// 判断某个类是否是Entrance
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsLibrarian(Type type)
        {
            bool result = typeof(ILibrarian).IsAssignableFrom(type);
            return result;
        }
        /// <summary>
        /// 从所有入口类中获取所有拓展模块包装器
        /// </summary>
        /// <param name="entrances"></param>
        /// <returns></returns>
        private IEnumerable<IExtensionWarpper> GetWarppersFrom(IEnumerable<ILibrarian> libs)
        {
            List<IExtensionWarpper> result = new List<IExtensionWarpper>();
            foreach (var lib in libs)
            {
                try
                {
                    result.AddRange(lib.GetWarppers());
                }
                catch (Exception ex)
                {
                    Logger.Warn($"获取拓展模块封装类失败({lib.Name})", ex);
                }
            }
            return result;
        }
        /// <summary>
        ///析构
        /// </summary>
        ~InternalManagerImpl()
        {
            foreach (var lib in Librarians)
            {
                try
                {
                    lib.Destory();
                }
                catch (Exception ex)
                {
                    Logger.Warn("Destoring failed", ex);
                }
            }
        }
    }
}
