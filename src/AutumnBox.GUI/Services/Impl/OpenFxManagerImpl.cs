using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.OpenFramework.Management.Wrapper;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IOpenFxManager))]
    class OpenFxManagerImpl : IOpenFxManager
    {
        public IExtensionTask[] RunningTasks => OpenFx.Lake.Get<IExtensionTaskManager>().RunningTasks.ToArray();

        public IExtensionWrapper[] ExtensionWrappers => OpenFx.Lake.Get<ILibsManager>().Wrappers().ToArray();

        private ILogger logger = LoggerFactory.Auto(nameof(OpenFxManagerImpl));
        private readonly Queue<Action> handlers = new Queue<Action>();
        private bool isLoaded = false;
        public void LoadOpenFx()
        {
            logger.Info("Initializing open framework api");
            OpenFx.Load(new AutumnBoxGuiBaseApiImpl());
            logger.Info("Open framework api system is initialized");
            logger.Info("Loading extensions");
            OpenFx.RefreshExtensionsList();
            ILibsManager libsManager = OpenFx.Lake.Get<ILibsManager>();
            logger.Info($"There are {libsManager.Librarians.Count()} librarians and {libsManager.Wrappers().Count()} wrappers");
            isLoaded = true;
            while (handlers.Any())
            {
                try
                {
                    handlers.Dequeue()();
                }
                catch (Exception e)
                {
                    logger.Warn("", e);
                }
            }
        }

        public void RunExtension(string extensionClassName)
        {
            try
            {
                LakeProvider.Lake.Get<ITaskManager>().CreateNewTaskOf(extensionClassName)?.Start();
            }
            catch (Exception e)
            {
                logger.Warn($"Can't start extension '{extensionClassName}'", e);
            }
        }

        public void WakeIfLoaded(Action callback)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (isLoaded) callback();
            else handlers.Enqueue(callback);
        }
    }
}
