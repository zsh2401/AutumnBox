#nullable enable
using AutumnBox.Leafx.Container;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management.ExtInfo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Management.ExtTask
{
    internal static class ExtensionTaskFactory
    {
        internal static Task<object?> CreateTask(IExtensionInfo info,
            Dictionary<string, object> args,
            Action<Thread> threadReceiver,
            params ILake[] source)
        {
            return new Task<object?>(() =>
            {
                try
                {
                    Thread.CurrentThread.Name = $"extension-task-{info.Id}";
                    var procedure = info.Procedure;
                    threadReceiver(Thread.CurrentThread);
                    procedure.Source = source;
                    procedure.Args = args;
                    return procedure.Run();
                }
                catch (Exception e)
                {
                    SLogger.Warn($"ExtensionTask", "Uncaught error", e);
                    return default;
                }
            });
        }
    }
}
