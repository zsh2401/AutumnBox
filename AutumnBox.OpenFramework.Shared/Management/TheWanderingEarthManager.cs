/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 1:28:18 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.ExtLibrary;
using AutumnBox.OpenFramework.Service;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutumnBox.OpenFramework.Management
{

    /// <summary>
    /// 拓展模块管理器
    /// </summary>
    internal sealed class TheWanderingEarthManager : Context, ILibsManager
    {
        public const string PATTERN_DFT_EXT = "*.dll";
        public const string PATTERN_DFT_ATMBEXT = "*.atmb";
        public const string PATTERN_DFT_HEXT = "*.adll";
        public const string PATTERN_ONCE_EXT = "*.odll";
        private readonly string extDir;
        private Assembly[] onceAssemblies;
        public bool IsOnceAssembly(Assembly assembly)
        {
            return onceAssemblies.Where((ass) =>
            {
                return ass == assembly;
            }).Count() != 0;
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
        public void Load()
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
        }
        /// <summary>
        /// 拓展文件夹检查
        /// </summary>
        private void DirCheck()
        {
            DirectoryInfo dir = new DirectoryInfo(extDir);
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
            DirectoryInfo dir = new DirectoryInfo(extDir);
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
                    var assembly = Assembly.LoadFile(file.FullName);
                    //AppDomain.CurrentDomain.
                    buffer.Add(assembly);
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
            return result;
        }

        public TheWanderingEarthManager(string extDir)
        {
            this.extDir = extDir ?? throw new ArgumentNullException(nameof(extDir));
        }

        /// <summary>
        ///析构
        /// </summary>
        ~TheWanderingEarthManager()
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
