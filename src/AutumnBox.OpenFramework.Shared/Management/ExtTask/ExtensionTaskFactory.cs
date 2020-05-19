#nullable enable
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Management.ExtTask
{

    internal static class ExtensionTaskFactory
    {
        [ClassText("error_title_fmt", "An error occurred in {0}", "zh-CN:{0}发生错误")]
        private class T { }
        internal static Task<object?> CreateTask(IExtensionInfo info,
            Dictionary<string, object> args,
            Action<Thread> threadReceiver,
            ILake source)
        {
            return new Task<object?>(() =>
            {
                try
                {
                    Thread.CurrentThread.Name = $"Extension Task {info.Id}";
                    using var procedure = info.OpenProcedure();
                    threadReceiver(Thread.CurrentThread);
                    procedure.Source = source;
                    procedure.Args = args;
                    return procedure.Run();
                }
                catch (Exception e)
                {
                    SLogger.Warn("ExtensionTask", "Uncaught error", e);
                    var title = string.Format(new T().RxGetClassText("error_title_fmt"), info.Name());
                    LakeProvider.Lake.Get<IAppManager>().ShowException(title, e.GetType().Name, e);
                    return default;
                }
            });
        }
    }
}
