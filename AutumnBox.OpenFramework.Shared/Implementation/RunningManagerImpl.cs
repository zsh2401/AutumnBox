using System;
using System.Linq;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtensionThreading;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Implementation
{
    internal class RunningManagerImpl : ITaskManager
    {
        public IExtensionTask[] Tasks => throw new NotImplementedException();

        public IExtensionTask CreateNewTaskOf(string extensionClassName)
        {
            throw new NotImplementedException();
        }

        public IExtensionTask CreateNewTaskOf<T>() where T : IExtension
        {
            throw new NotImplementedException();
        }

        public IExtensionTask CreateNewTaskOf(Type t)
        {
            throw new NotImplementedException();
        }

        public IExtensionThread GetNewThread(string extensionClassName)
        {
            var wrappers = from wrapper in OpenFxLoader.LibsManager.Wrappers()
                           where extensionClassName == wrapper.Info.ExtType.Name
                           select wrapper;
            if (!wrappers.Any())
            {
                throw new Exception("Extension not found");
            }
            return wrappers.First().GetThread();
        }

        public IExtensionThread[] GetRunningThreads()
        {
            return ExtensionThreadManager.Instance.GetRunning().ToArray();
        }
    }
}
