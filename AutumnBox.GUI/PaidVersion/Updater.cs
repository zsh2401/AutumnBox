/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/26 0:07:24 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.PaidVersion
{
    static class Updater
    {
        public static DVUpdateInfo Check()
        {
            return new DVUpdateChecker().Run();
        }
        public static void CheckAsync(Action<DVUpdateInfo> callback)
        {
            new DVUpdateChecker().RunAsync(callback);
        }
        public static void DeleteUpdaterTemp()
        {
            try { File.Delete("../AutumnBox.Updater.exe"); } catch { }
        }
        public static void RunUpdater()
        {
            File.Copy("AutumnBox.Updater.exe", "../AutumnBox.Updater.exe", true);
            Process.Start(new ProcessStartInfo()
            {
                WorkingDirectory = "..",
                FileName = "AutumnBox.Updater.exe"
            });
        }
    }
}
