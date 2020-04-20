using AutumnBox.OpenFramework.Management.ExtInfo;
using System;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    interface IOpenFxManager
    {
        void RunExtension(string extensionClassName);
        void LoadOpenFx();
        void WakeIfLoaded(Action callback);
        Task<object>[] RunningTasks { get; }
        IExtensionInfo[] Extensions { get; }
    }
}
