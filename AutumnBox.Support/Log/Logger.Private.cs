/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/31 12:16:48 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Support.Log
{

    partial class Logger
    {
        private static readonly object _logLock = new object();
        private static void Log(string tag, string suffix, string content,
            bool writeToFile, string fileName)
        {
            lock (_logLock)
            {
                var str = MakeString(tag, suffix, content);
                if (writeToFile)
                {
                    //write to file
				}
            }
        }
        private static string GetSavePath(int up = 3) {
            throw new NotImplementedException();
        }
        private static string MakeString(string tag, string suffix, string content)
        {
            var timeStr = DateTime.Now.ToString("yy-MM-dd hh:mm:ss");
            return $"[{timeStr}][{tag}/{suffix}]:{content}";
        }
    }
}
