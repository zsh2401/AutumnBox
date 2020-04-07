using AutumnBox.OpenFramework.Leafx;
using AutumnBox.OpenFramework.Leafx.Attributes;
using AutumnBox.OpenFramework.Leafx.ObjectManagement;
using System;
using System.Reflection;

namespace AutumnBox.OpenFramework.Management.ExtLibrary.Impl
{
    internal sealed class LibrarianBuilder : ILibrarianBuilder
    {
        [AutoInject]
        private ILake Lake { get; set; }

        public ILibrarian BuildCustom(Type libType)
        {
            if (libType == null)
            {
                throw new ArgumentNullException(nameof(libType));
            }
            var builder = new ObjectBuilder(libType, Lake);
            return (ILibrarian)builder.Build();
        }

        public ILibrarian BuildDefault(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            return new DefaultLibrarian(assembly);
        }
    }
}
