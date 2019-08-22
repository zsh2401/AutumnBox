using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.ExtLibrary;

namespace AutumnBox.OpenFramework.Management
{
    internal class OnceLoader : ILibsManager
    {
        private const string PATTERN_DEFAULT = "*.dll";
        private const string PATTERN_ATMBEXT = "*.aext";
        private const string PATTERN_OEXT = "*.aoext";
        public IEnumerable<ILibrarian> Librarians { get; private set; }

        public void Load()
        {
            Librarians = Ready(Check(GetLibManagers(GetAssemblies(GetFiles()))));
        }
        private IEnumerable<ILibrarian> Check(IEnumerable<ILibrarian> libs)
        {
            return from lib in libs
                   where SafeCheck(lib)
                   select lib;
        }
        private bool SafeCheck(ILibrarian lib) {
            try {
                return lib.Check();
            } catch (Exception e){
                SLogger<OnceLoader>.Warn($"An error occurred when checking a librarian",e);
                return false;
            }
        }
        private bool SafeReady(ILibrarian lib) {
            try
            {
                lib.Ready();
                lib.Reload();
                return true;
            }
            catch (Exception e)
            {
                SLogger<OnceLoader>.Warn($"There is an error occurred when calling a librarian's Ready() method", e);
                return false;
            }
        }
        private IEnumerable<ILibrarian> Ready(IEnumerable<ILibrarian> libs)
        {
            return from lib in libs
                   where SafeReady(lib)
                   select lib;
        }
        private IEnumerable<FileInfo> GetFiles()
        {
            var extDir = new DirectoryInfo(BuildInfo.DEFAULT_EXTENSION_PATH);
            var files =  new List<FileInfo>()
                .Concat(extDir.GetFiles(PATTERN_DEFAULT))
                .Concat(extDir.GetFiles(PATTERN_ATMBEXT))
                .Concat(extDir.GetFiles(PATTERN_OEXT));
            SLogger<OnceLoader>.Debug($"There are {files.Count()} extension file");
            return files;
        }
        private IEnumerable<Assembly> GetAssemblies(IEnumerable<FileInfo> files)
        {
            var result = new List<Assembly>();
            foreach (var file in files)
            {
                try
                {
                    if (file.Extension == PATTERN_OEXT)
                    {
                        result.Add(Assembly.LoadFile(file.FullName));
                    }
                    else
                    {
                        result.Add(Assembly.Load(File.ReadAllBytes(file.FullName)));
                    }
                }
                catch (Exception e)
                {
                    SLogger<OnceLoader>.Warn($"can not load extension: {file.Name}", e);
                }
            }
            SLogger<OnceLoader>.Debug($"There are {result.Count()} assemblies");
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
                        result.Add((ILibrarian)Activator.CreateInstance(libManagerTypes.First()));
                    }
                    else
                    {
                        result.Add(new DefaultLibrarian(assembly));
                    }
                }
                catch (Exception e)
                {
                    SLogger<OnceLoader>.Warn($"Can not create the instance of {assembly.GetName().Name}'s librarian", e);
                }
            }
            return result;
        }
    }
}
