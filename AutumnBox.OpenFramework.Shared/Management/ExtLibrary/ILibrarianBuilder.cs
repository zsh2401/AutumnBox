using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AutumnBox.OpenFramework.Management.ExtLibrary
{
    internal interface ILibrarianBuilder
    {
        ILibrarian BuildDefault(Assembly assembly);
        ILibrarian BuildCustom(Type libType);
    }
}
