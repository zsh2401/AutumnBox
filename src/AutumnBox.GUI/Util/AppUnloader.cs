/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 22:46:43 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic;
using AutumnBox.GUI.Services;
using AutumnBox.Logging.Management;
using AutumnBox.OpenFramework.Management;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutumnBox.GUI.Util
{
    internal static class AppUnloader
    {
        public static void Unload()
        {
            Analytics.TrackEvent("App Shutdown", new Dictionary<string, string>()
            {
                { "Run Time", (DateTime.Now -  Process.GetCurrentProcess().StartTime).ToString()},
            });

            try { App.Current.Lake.GetComponent<ISettings>().Save(); } catch { }
            try { OpenFx.Unload(); } catch { }
            try { BasicBooter.Free(); } catch { }
            try { LoggingManager.Free(); } catch { }
        }
    }
}
