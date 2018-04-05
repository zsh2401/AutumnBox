/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/24 20:12:08 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.IO;
using System.Linq;

namespace AutumnBox.Support.Log
{
    partial class Logger
    {
        private const string logTemplate = "[{0}][{1}/{2}]: {3}";
        private const string LOG_FLODER = "..\\logs";
        private const string LOG_FILENAME_FORMAT = "yy_MM_dd__HH_mm_ss";
        private static readonly StreamWriter logFileWriter;
        static Logger()
        {
            if (Directory.Exists(LOG_FLODER) == false) Directory.CreateDirectory(LOG_FLODER);
            string dateTime = DateTime.Now.ToString(LOG_FILENAME_FORMAT);
            logFileWriter = new StreamWriter(Path.Combine(LOG_FLODER, $"{dateTime}.log"))
            {
                AutoFlush = true
            };
            DeleteObsoleteLog();
        }
        private static void DeleteObsoleteLog()
        {
            var obsoleteLog = from file in new DirectoryInfo(LOG_FLODER).GetFiles("*.log")
                              where (file.CreationTime - DateTime.Now).TotalDays > 3
                              select file;
            foreach (var logFile in obsoleteLog)
            {
                logFile.Delete();
            }
        }
        public static string MakeText(object sender, object prefix, object message, Exception ex = null)
        {
            string tag;
            if (sender == null)
                tag = "Anonymous";
            else if (sender is string tagString)
                tag = tagString;
            else
                tag = sender.GetType().Name;
            return string.Format(logTemplate,
                DateTime.Now.ToString("yy-MM-dd HH:mm:ss"), tag, prefix, message + ex?.ToString() + Environment.NewLine);
        }
        private static void WriteToFile(string msg)
        {
            logFileWriter.Write(msg);
            Logged?.Invoke(new object(), new LogEventArgs(msg));
        }
    }
}
