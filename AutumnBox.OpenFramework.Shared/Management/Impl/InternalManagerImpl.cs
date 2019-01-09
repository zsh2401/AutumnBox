/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:28:18 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Service;
using AutumnBox.OpenFramework.Wrapper;
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
    [ServiceName(SERVICE_NAME)]
    internal sealed class InternalManagerImpl : AtmbService, IInternalManager
    {
        public const string SERVICE_NAME = "InternalManager";
        public const string PATTERN_DFT_EXT = "*.dll";
        public const string PATTERN_DFT_ATMBEXT = "*.atmb";
        public const string PATTERN_DFT_HEXT = "*.adll";
        public const string PATTERN_ONCE_EXT = "*.odll";

        private Assembly[] onceAssemblies;
        public bool IsOnceAssembly(Assembly assembly)
        {
            return onceAssemblies.Where((ass) =>
            {
                return ass == assembly;
            }).Count() != 0;
        }
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
        public IEnumerable<ILibrarian> Librarians { get; private set; } = null;
        /// <summary>
        /// 已加载的所有包装器
        /// </summary>
        public IEnumerable<IExtensionWrapper> Wrappers => GetWrappersFrom(Librarians);
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => "ExntesionManager";
        /// <summary>
        /// 重新加载所有拓展模块
        /// </summary>
        public void Reload()
        {
            DirCheck();
            if (Librarians == null)
            {
                GetLibs();
            }
            if (Librarians.Count() > 0)
            {
                ReloadLibs();
            }
            Logger.Info($"loaded {Librarians.Count()} librarian and {Wrappers.Count()} Wrappers");
        }
        /// <summary>
        /// 拓展文件夹检查
        /// </summary>
        private void DirCheck()
        {
            Logger.Info("checking ext floder");
            DirectoryInfo dir = new DirectoryInfo(ExtensionPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }
        /// <summary>
        /// 获取文件夹下程序集的管理器
        /// </summary>
        private void GetLibs()
        {

            DirectoryInfo dir = new DirectoryInfo(ExtensionPath);
            List<FileInfo> dllFiles = new List<FileInfo>();
            dllFiles.AddRange(dir.GetFiles(PATTERN_DFT_EXT));
            dllFiles.AddRange(dir.GetFiles(PATTERN_DFT_HEXT));
            dllFiles.AddRange(dir.GetFiles(PATTERN_DFT_ATMBEXT));
            FileInfo[] odlls = dir.GetFiles(PATTERN_ONCE_EXT);
            IEnumerable<Assembly> assemblies = LoadAssemblies(dllFiles.ToArray(), odlls);
            Librarians = GetLibrarianFrom(assemblies);
        }
        /// <summary>
        /// 执行已加载的管理器的Reload()
        /// </summary>
        private void ReloadLibs()
        {
            if (Librarians == null) return;
            foreach (var lib in Librarians)
            {
                try
                {
                    lib.Reload();
                }
                catch (Exception ex)
                {
                    Logger.Warn($"{lib.Name}.Reload() was failure", ex);
                }
            }
        }
        /// <summary>
        /// 加载所有程序集
        /// </summary>
        /// <returns>加载完毕的程序集</returns>
        private IEnumerable<Assembly> LoadAssemblies(FileInfo[] normalFiles, FileInfo[] onceFiles)
        {
            List<Assembly> result = new List<Assembly>();
            NormalLoad(result, normalFiles);
            ODllLoad(result, onceFiles);
            return result;
        }
        /// <summary>
        /// 加载普通模块
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="dllFiles"></param>
        private void NormalLoad(List<Assembly> buffer, FileInfo[] dllFiles)
        {
            foreach (var file in dllFiles)
            {
                try
                {
                    var data = File.ReadAllBytes(file.FullName);
                    buffer.Add(AppDomain.CurrentDomain.Load(data));
                }
                catch (Exception ex)
                {
                    Logger.Warn($"加载拓展模块失败({file.Name})", ex);
                }
            }
        }
        /// <summary>
        /// 加载全载型模块
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="odllFiles"></param>
        private void ODllLoad(List<Assembly> buffer, FileInfo[] odllFiles)
        {
            List<Assembly> onceAssemblies = new List<Assembly>();
            foreach (var file in odllFiles)
            {
                try
                {
                    byte[] data = File.ReadAllBytes(file.FullName);
                    Assembly assembly = AppDomain.CurrentDomain.Load(data);
                    buffer.Add(assembly);
                    onceAssemblies.Add(assembly);
                }
                catch (Exception ex)
                {
                    Logger.Warn($"无占用加载拓展模块失败({file.Name})", ex);
                }
            }
            this.onceAssemblies = onceAssemblies.ToArray();
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
                    ILibrarian lib = GetLibrarianFrom(ass);
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
        private ILibrarian GetLibrarianFrom(Assembly assembly)
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
        /// <param name="libs"></param>
        /// <returns></returns>
        private IEnumerable<IExtensionWrapper> GetWrappersFrom(IEnumerable<ILibrarian> libs)
        {
            List<IExtensionWrapper> result = new List<IExtensionWrapper>();
            foreach (var lib in libs)
            {
                try
                {
                    result.AddRange(lib.GetWrappers());
                }
                catch (Exception ex)
                {
                    Logger.Warn($"获取拓展模块封装类失败({lib.Name})", ex);
                }
            }
            ////筛选出未被创建过的Wrappers
            //var filtedResult = from wp in result
            //                   where cacheWrappers.IndexOf(wp) == -1
            //                   select wp;

            return result;
        }

        private bool IsOk(IExtensionWrapper Wrapper, IWrapperFilter[] filters)
        {
            foreach (var filter in filters)
            {
                try
                {
                    if (!filter.DoFilter(Wrapper))
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Logger.Warn($"can not filter extension", e);
                    return false;
                }

            }
            return true;
        }

        public IEnumerable<IExtensionWrapper> GetLoadedWrappers(params IWrapperFilter[] filters)
        {
            List<IExtensionWrapper> all = Wrappers.ToList();
            return from w in all
                   where IsOk(w, filters)
                   orderby (int)w.Info[ExtensionInformationKeys.PRIORITY] descending
                   select w;
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
