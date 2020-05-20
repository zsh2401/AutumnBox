using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;
using System;
using System.Reflection;

namespace AutumnBox.OpenFramework.Management
{
    internal sealed class ManagementObjectBuilder : IManagementObjectBuilder
    {
        [AutoInject]
        private ILake Lake { get; set; }

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
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            var builder = new ObjectBuilder(typeof(AssemblyLibrarian), Lake);
            return (AssemblyLibrarian)builder.Build();
        }
    }
}
