using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
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
                threadReceiver(Thread.CurrentThread);
                IClassExtension classExtension = (IClassExtension)new ObjectBuilder(extType, source).Build();
                return classExtension.Main(extralArgs);
            });
        }
    }
}
