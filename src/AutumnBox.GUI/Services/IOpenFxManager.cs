using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.OpenFramework.Management.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    interface IOpenFxManager
    {
        void RunExtension(string extensionClassName);
        void LoadOpenFx();
        void WakeIfLoaded(Action callback);
        IExtensionTask[] RunningTasks { get; }
        IExtensionWrapper[] ExtensionWrappers { get; }
    }
}
