/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/31 12:12:45 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Support.Log
{
    public partial class Logger
    {
        private readonly string Tag;
        private readonly LogSettingsAttribute logSettings;
        public Logger(object sender)
        {
            Tag = sender.GetType().Name;
            logSettings = GetSettings(sender);
        }
        public Logger(string tag, object anyObjectOncallerAssembly)
        {
            Tag = tag;
            logSettings = GetSettings(anyObjectOncallerAssembly);
        }
        public void D(object content)
        {
            if (!logSettings.IsInDebugMode) return;
            var fullString = MakeString(Tag, "debug", content.ToString());
            Debug.WriteLine(fullString);
            WriteToFile(logSettings.FileName, fullString);
        }
        public void T(object content)
        {
            var fullString = MakeString(Tag, "trace", content.ToString());
            Trace.WriteLine(fullString);
            WriteToFile(logSettings.FileName, fullString);
        }
    }
}
