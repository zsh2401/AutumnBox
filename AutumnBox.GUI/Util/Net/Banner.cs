using AutumnBox.GUI.Util.Net.Getters;
using AutumnBox.Logging;
using System;
using System.Windows;

namespace AutumnBox.GUI.Util.Net
{
    static class Banner
    {
        private const string TAG = nameof(Banner);
        public static string Reason { get; private set; }
        public static void Check()
        {
            SLogger.Debug(TAG, "doing banned-check");
            new VersionBanInfoGetter().Advance().ContinueWith((task) =>
            {
                SLogger.Debug(TAG, "the banned-check task is over");
                SLogger.Debug(TAG, $"tsuc:{task.IsCompleted} trst:{(task?.Result?.Json ?? "null")}");
                if (task.IsCompleted)
                {
                    var result = task.Result;
                    if (result.Banned)
                    {
                        Reason = result.Reason;
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            App.Current.Shutdown(App.ERR_BANNED_VERSION);
                        });
                    }
                }
            });
        }
    }
}
