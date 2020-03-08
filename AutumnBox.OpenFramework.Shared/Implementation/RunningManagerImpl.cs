using System;
using System.Linq;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class RunningManagerImpl : ITaskManager
    {
        public IExtensionTask[] Tasks => ExtensionTaskManager.Instance.RunningTasks.ToArray();

        public IExtensionTask CreateNewTaskOf(string extensionClassName)
        {
            var wrappers = from wrapper in OpenFxLoader.LibsManager.Wrappers()
                           where extensionClassName == wrapper.Info.ExtType.Name
                           select wrapper;
            if (!wrappers.Any())
            {
                throw new Exception("Extension not found");
            }
            return new ExtensionTask(wrappers.First().ExtensionType);
        }

        public IExtensionTask CreateNewTaskOf<T>() where T : IExtension
        {
            return CreateNewTaskOf(typeof(T));
        }

        public IExtensionTask CreateNewTaskOf(Type t)
        {
            var wrappers = from wrapper in OpenFxLoader.LibsManager.Wrappers()
                           where t == wrapper.Info.ExtType
                           select wrapper;
            if (!wrappers.Any())
            {
                throw new Exception("Extension not found");
            }
            return new ExtensionTask(wrappers.First().ExtensionType);
        }
    }
}
