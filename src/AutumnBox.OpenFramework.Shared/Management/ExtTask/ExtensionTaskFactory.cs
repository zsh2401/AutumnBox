using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Management.ExtTask
{
    internal static class ExtensionTaskFactory
    {
        internal static Task<object> CreateTask(Type extType,
            Dictionary<string, object> extralArgs,
            Action<Thread> threadReceiver,
            params ILake[] source)
        {
            return new Task<object>(() =>
            {
                try
                {
                    threadReceiver(Thread.CurrentThread);
                    IExtension classExtension = (IExtension)new ObjectBuilder(extType, source).Build();
                    return classExtension.Main(extralArgs);
                }
                catch (Exception e)
                {
                    SLogger.Warn("ExtensionTask", "Uncaught error", e);
                    return default;
                }
            });
        }
    }
}
