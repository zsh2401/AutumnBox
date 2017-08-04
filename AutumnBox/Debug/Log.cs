using System;
using System.Diagnostics;
using System.IO;

namespace AutumnBox.Debug
{
    public static class Log
    {
        static string LogPath = "atb.log";
        public static void d(string tag, string content)
        {
            string message = $"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {tag} : {content}";
            Trace.WriteLine(message);
            WriteToLogFile(message);
        }
        public static void l(string tag, string content)
        {
            string message = $"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {tag} : {content}";
            Trace.WriteLine(message);
        }
        public static void InitLogFile()
        {
            StreamWriter sw = new StreamWriter(LogPath, true);
            sw.WriteLine("");
            sw.Flush();
            sw.Close();
        }

        static void WriteToLogFile(string message)
        {
            StreamWriter sw = new StreamWriter(LogPath, true);
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
        }

    }
}
