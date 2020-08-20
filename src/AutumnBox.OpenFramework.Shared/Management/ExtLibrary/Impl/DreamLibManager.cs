using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;

namespace AutumnBox.OpenFramework.Management.ExtLibrary.Impl
{
    /// <summary>
    /// LibsManager的主要实现
    /// 用于对所有拓展模块实现一站式管理
    /// </summary>
    internal sealed class DreamLibManager : ILibsManager
    {
        [AutoInject] ILake Lake { get; set; }

        public event EventHandler? ExtensionRegistryModified;

        public IEnumerable<ILibrarian> Librarians { get; private set; } = new ILibrarian[0];

        /// <summary>
        /// 公开获取注册表的方法
        /// </summary>
        public IExtensionRegistry Registry => registry;
        readonly RegisterImpl registry = new RegisterImpl();

        /// <summary>
        /// 内部的注册表实现
        /// </summary>
        private class RegisterImpl : ObservableCollection<IRegisteredExtensionInfo>, IExtensionRegistry { }

        /// <summary>
        /// 构造DreamLibManager
        /// </summary>
        public DreamLibManager()
        {
            registry.CollectionChanged += (s, e) =>
            {
                Task.Run(() =>
                {
                    ExtensionRegistryModified?.Invoke(this, new EventArgs());
                });
            };
        }

        public void Initialize()
        {
            Librarians = ReloadLibs(Check(GetLibManagers(GetAssemblies(GetFiles()))));
            Librarians.All((lib) =>
            {
                SLogger<DreamLibManager>.Info($"Calling ready method: {lib.Name}");
                SafeReady(lib);
                return true;
            });
        }

        private IEnumerable<ILibrarian> ReloadLibs(IEnumerable<ILibrarian> libs)
        {
            List<ILibrarian> reloaded = new List<ILibrarian>();
            foreach (var lib in libs)
            {
                try
                {
                    lib.Reload();
                    reloaded.Add(lib);
                }
                catch (Exception e)
                {
                    SLogger<DreamLibManager>.Warn($"An error occured when reloading lib {lib?.GetType().Name}", e);
                }
            }
            return reloaded;
        }

        private IEnumerable<ILibrarian> Check(IEnumerable<ILibrarian> libs)
        {
            return from lib in libs
                   where SafeCheck(lib)
                   select lib;
        }

        private bool SafeCheck(ILibrarian lib)
        {
            try
            {
                return lib.Check();
            }
            catch (Exception e)
            {
                SLogger<DreamLibManager>.Warn($"An error occurred when checking a librarian", e);
                return false;
            }
        }

        private bool SafeReady(ILibrarian lib)
        {
            try
            {
                lib.Ready();
                return true;
            }
            catch (Exception e)
            {
                SLogger<DreamLibManager>.Warn($"There is an error occurred when calling a librarian's Ready() method", e);
                return false;
            }
        }

        private IEnumerable<FileInfo> GetFiles()
        {
            var extDir = BuildInfo.ExtensionStore;
            if (!extDir.Exists) extDir.Create();
            List<FileInfo> files = new List<FileInfo>();

            foreach (var file in extDir.GetFiles())
            {
                SLogger<DreamLibManager>.Info($"Found file: {file.Name}");
            }

            var extensionFiles = from file in extDir.GetFiles()
                                 where file.Extension == ".dll"
                                 select file;

            files.AddRange(extensionFiles);
            SLogger<DreamLibManager>.Debug($"There are {files.Count()} extension file");
            return files;
        }

        private IEnumerable<Assembly> GetAssemblies(IEnumerable<FileInfo> files)
        {
            var result = new List<Assembly>();
            foreach (var file in files)
            {
                try
                {
                    result.Add(Assembly.LoadFile(file.FullName));
                    //if (file.Extension == PATTERN_OEXT.Substring(1))
                    //{
                    //    SLogger<DreamLibManager>.Info($"{file} is an aoext");
                    //    result.Add(Assembly.LoadFile(file.FullName));
                    //}
                    //else
                    //{
                    //    result.Add(Assembly.Load(File.ReadAllBytes(file.FullName)));
                    //}
                }
                catch (Exception e)
                {
                    SLogger<DreamLibManager>.Warn($"Could not load assembly: {file.Name}", e);
                }
            }
            SLogger<DreamLibManager>.Debug($"There are {result.Count()} assemblies");
            return result;
        }

        private IEnumerable<ILibrarian> GetLibManagers(IEnumerable<Assembly> assemblies)
        {
            var result = new List<ILibrarian>();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var libManagerTypes = (from type in assembly.GetTypes()
                                           where typeof(ILibrarian).IsAssignableFrom(type)
                                           select type);

                    if (libManagerTypes.Any())
                    {
                        result.Add(BuildLibrarian(libManagerTypes.First()));
                    }
                    else
                    {
                        result.Add(BuildLibrarian(assembly));
                    }
                }
                catch (Exception e)
                {
                    SLogger<DreamLibManager>.Warn($"Could not create the instance of {assembly.GetName().Name}'s librarian", e);
                }
            }
            SLogger<DreamLibManager>.Info($"There are {result.Count()} librarians");
            foreach (var lib in result)
            {
                SLogger<DreamLibManager>.Info($"{lib.Name}");
            }
            return result;
        }

        public ILibrarian BuildLibrarian(Type libType)
        {
            if (libType == null)
            {
                throw new ArgumentNullException(nameof(libType));
            }
            var builder = new ObjectBuilder(libType, Lake);
            return (ILibrarian)builder.Build();
        }

        public ILibrarian BuildLibrarian(Assembly assembly)
        {
            var builder = new ObjectBuilder(typeof(AssemblyLibrarian), Lake);
            var librarian =  (AssemblyLibrarian)builder.Build();
            librarian.ManagedAssembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            return librarian;
        }
    }
}
