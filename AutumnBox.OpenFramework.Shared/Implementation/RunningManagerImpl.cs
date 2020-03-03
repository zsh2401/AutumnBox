using System;
using System.Linq;
using AutumnBox.OpenFramework.Fast;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Running;

namespace AutumnBox.OpenFramework.Implementation
{
    internal  class RunningManagerImpl : IRunningManager
    {
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
