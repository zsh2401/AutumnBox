using AutumnBox.Reporter.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Reporter.Util
{
    static class LogsScanner
    {
        public const string LOGS_FOLDER_PATH = "logs";
        public static IEnumerable<Log> Scan(string path = LOGS_FOLDER_PATH)
        {
            DirectoryInfo dir = new DirectoryInfo(LOGS_FOLDER_PATH);
            List<Log> result = new List<Log>();
            if (dir.Exists == false)
            {
                return result;
            }
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                result.Add(new Log(file));
            }
            return result;
        }
    }
}
