/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/14 20:50:16 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AutumnBox.Support.Log
{
    public partial class Logger
    {
        private static readonly object _writeLock = new object();
        private static readonly string _rootPath;
        static Logger()
        {
            _rootPath = $"logs\\{DateTime.Now.ToString("MM_dd_yy")}";
            if (!Directory.Exists(_rootPath))
                Directory.CreateDirectory(_rootPath);
        }
        private static string MakeString(string TAG, string suffix, string message)
        {
            return $"[{DateTime.Now.ToString("HH:mm:ss")}][{TAG}/{suffix}]: {message}";
        }
        private static void WriteToFile(string fileName, string content)
        {
            lock (_writeLock)
            {
                using (StreamWriter writer = new StreamWriter(
                    _rootPath + "\\" + fileName,
                    true))
                {
                    writer.WriteLine(content);
                    writer.Flush();
                }
            }
        }
        private static LogSettingsAttribute GetSettings(object sender)
        {
            var attrs = sender.GetType().Assembly.GetCustomAttributes(true);
            var logSettingAttr = attrs.ToList().Find((attr) =>
            {
                return attr is LogSettingsAttribute;
            });
            if (logSettingAttr != null)
            {
                return logSettingAttr as LogSettingsAttribute;
            }
            else
            {
                return new LogSettingsAttribute();
            }
        }
    }
}
