using AutumnBox.OpenFramework.Management.ExtTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    interface IOpenFxManager
    {
        IExtensionTask RunExtension(string extensionClassName);
        void LoadOpenFx();
        IExtensionTask[] RunningTasks { get; }
    }
}
