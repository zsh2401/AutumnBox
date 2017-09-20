using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.SharedTools
{
    public static class Logger
    {
        public static string ToFullMessage(string tag, string message) {
            string t = $"[{DateTime.Now.ToString("[yy-MM-dd hh:mm:ss]")}";
            return $"{t} { tag} : {message}"; 
        }
        public static void WriteToFile(string fileName,string fullMessage) {
            try
            {
                StreamWriter sw = new StreamWriter(fileName, true);
                sw.WriteLine(fullMessage);
                sw.Flush();
                sw.Close();
            }
            catch {}
        }
    }
}
