/*

* ==============================================================================
*
* Filename: OpenFxManagerImpl
* Description: 
*
* Version: 1.0
* Created: 2020/5/30 10:43:56
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace AutumnBox.OpenFramework.Implementation
{
    [Component(Type = typeof(IOpenFxManager))]
    class OpenFxManagerImpl : IOpenFxManager
    {
        readonly ILibsManager libsManager;

        public OpenFxManagerImpl(ILibsManager libsManager)
        {
            this.libsManager = libsManager;
            libsManager.ExtensionRegistryModified += (s, e) =>
            {
                ExtensionsChanged?.Invoke(this, new EventArgs());
            };
        }

        public IEnumerable<IExtensionInfo> Extensions => libsManager.Registry.Select(item => item.ExtensionInfo);

        public IEnumerable<ILibrarian> Librarians => libsManager.Librarians;

        public int SDKLevel => BuildInfo.API_LEVEL;

        public Version OpenFxVersion => BuildInfo.SDK_VERSION;

        public event EventHandler? ExtensionsChanged;

        public ILibrarian LibrarianOf(object context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            return (from rext in libsManager.Registry
                    where (rext.ExtensionInfo is ClassExtensionInfo ceInf && ceInf.ClassExtensionType == context.GetType())
                    select rext.Librarian).FirstOrDefault();
        }

        public void RaiseExtensionsChangedEvent()
        {
            ExtensionsChanged?.Invoke(this, new EventArgs());
        }
    }
}
