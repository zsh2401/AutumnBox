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
    [ClassText("error_title_fmt", "An error occurred in \"{0}\"", "zh-CN:\"{0}\"发生错误")]
    internal static class ExtensionTaskFactory
    {
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

                    TrackRunExtension(info);

                    return procedure.Run();
                }
                catch (Exception e)
                {
                    SLogger.Warn("ExtensionTask", "Uncaught error", e);

                    Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Extension Error", new Dictionary<string, string>()
                    {
                        { "Name",info.Name()},
                        { "Id",info.Id},
                        { "Exception",e.ToString()}
                    });

                    var text = ClassTextReaderCache.Acquire(typeof(ExtensionTaskFactory));
                    var extFullName = info.Name();
                    var extName = extFullName.Length > 7 ? extFullName.Substring(0, 6) + "..." : extFullName;
                    var title = string.Format(text.RxGet("error_title_fmt"), extName);
                    LakeProvider.Lake.Get<IAppManager>().ShowException(title, e.GetType().Name, e);
                    return default;
                }
            });
        }

        private static void TrackRunExtension(IExtensionInfo info)
        {
            try
            {
                switch (info.Id)
                {
                    case "EAutumnBoxAdFetcher":
                    case "EDonateCardRegister":
                    case "EAutumnBoxUpdateChecker":
                        break;
                    default:
                        Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Run Extension", new Dictionary<string, string>()
                            {
                                { "Name",info.Name()},
                                { "Id",info.Id},
                                { "Author",info.Author()}
                            });
                        break;
                }
            }
            catch { }
        }
    }
}
