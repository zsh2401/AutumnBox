using System;
using System.Linq;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Leafx.Container;
using AutumnBox.OpenFramework.Leafx.Attributes;
using AutumnBox.Logging;

namespace AutumnBox.OpenFramework.Implementation
{
    internal sealed class TaskManagerImpl : ITaskManager
    {
        [AutoInject]
        private IExtensionTaskManager ExtensionTaskManager { get; set; }

        public IExtensionTask[] Tasks
        {
            get
            {
                return ExtensionTaskManager.RunningTasks.ToArray();
            }
        }

        public IExtensionTask CreateNewTaskOf(string extensionClassName)
        {
            var wrappers = from wrapper in OpenFx.Lake.Get<ILibsManager>().Wrappers()
                           where extensionClassName == wrapper.Info.ExtType.Name
                           select wrapper;
            if (!wrappers.Any())
            {
                throw new ArgumentException("Extension not found");
            }
            return ExtensionTaskManager.Allocate(wrappers.First().ExtensionType);
        }

        public IExtensionTask CreateNewTaskOf<T>() where T : IExtension
        {
            return CreateNewTaskOf(typeof(T));
        }

        public IExtensionTask CreateNewTaskOf(Type t)
        {
            var wrappers = from wrapper in OpenFx.Lake.Get<ILibsManager>().Wrappers()
                           where t == wrapper.Info.ExtType
                           select wrapper;
            if (!wrappers.Any())
            {
                throw new ArgumentException("Extension not found");
            }
            return ExtensionTaskManager.Allocate(wrappers.First().ExtensionType);
        }
    }
}
