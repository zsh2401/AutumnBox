using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;

namespace AutumnBox.OpenFramework.Management.ExtLibrary.Impl
{
    internal sealed class DreamLibManager : ILibsManager
    {
        [AutoInject]
        private IManagementObjectBuilder mObjBuilder { get; set; }

        private const string PATTERN_DEFAULT = "*.dll";
        private const string PATTERN_ATMBEXT = "*.aext";
        private const string PATTERN_OEXT = "*.aoext";

        public IEnumerable<ILibrarian> Librarians { get; private set; }

        public void Reload()
        {
            Librarians = ReloadLibs(Check(GetLibManagers(GetAssemblies(GetFiles()))));
            Librarians.All((lib) =>
            {
                SLogger<DreamLibManager>.Warn($"Call ready method: {lib.Name}");
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
                SLogger<DreamLibManager>.Info($"{file.Name}{file.Extension}");
            }

            var extensionFiles = from file in extDir.GetFiles()
                                 where file.Extension == ".dll" || file.Extension == ".aext"
                                 select file;

            files.AddRange(extensionFiles);

#if DEBUG
            files.Add(new FileInfo("../../../../AutumnBox.Essentials/bin/Debug/netcoreapp3.1/AutumnBox.Essentials.dll"));
            files.Add(new FileInfo("../../../../AutumnBox.CoreModules/bin/Debug/netcoreapp3.1/AutumnBox.CoreModules.dll"));
#endif
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
                    if (file.Extension == PATTERN_OEXT.Substring(1))
                    {
                        SLogger<DreamLibManager>.Info($"{file} is an aoext");
                        result.Add(Assembly.LoadFile(file.FullName));
                    }
                    else
                    {
                        result.Add(Assembly.Load(File.ReadAllBytes(file.FullName)));
                    }
                }
                catch (Exception e)
                {
                    SLogger<DreamLibManager>.Warn($"can not load extension: {file.Name}", e);
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
                        result.Add(mObjBuilder.BuildLibrarian(libManagerTypes.First()));
                    }
                    else
                    {
                        result.Add(mObjBuilder.BuildLibrarian(assembly));
                    }
                }
                catch (Exception e)
                {
                    SLogger<DreamLibManager>.Warn($"Can not create the instance of {assembly.GetName().Name}'s librarian", e);
                }
            }
            SLogger<DreamLibManager>.Info($"Found {result.Count()} lib");
            return result;
        }
    }
}
