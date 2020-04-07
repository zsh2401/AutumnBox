using AutumnBox.OpenFramework.Leafx;
using AutumnBox.OpenFramework.Leafx.Attributes;
using AutumnBox.OpenFramework.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Management.ExtLibrary.Impl;
using AutumnBox.OpenFramework.Management.Wrapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.Management
{
    internal sealed class ManagementObjectBuilder : IManagementObjectBuilder
    {
        [AutoInject]
        private ILake Lake { get; set; }

        public IExtensionWrapper BuildExtensionWrapper(Type extensionType)
        {
            var builder = new ObjectBuilder(typeof(ClassExtensionWrapper), Lake);
            var args = new Dictionary<string, object>() { { "t", extensionType } };
            return (IExtensionWrapper)builder.Build(args);
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
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            return new DefaultLibrarian(assembly);
        }
    }
}
