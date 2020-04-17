using AutumnBox.OpenFramework.Management.ExtLibrary;
using System;
using System.Reflection;

namespace AutumnBox.OpenFramework.Management
{
    internal interface IManagementObjectBuilder
    {
        ILibrarian BuildLibrarian(Assembly assembly);
        ILibrarian BuildLibrarian(Type librarianType);
    }
}
