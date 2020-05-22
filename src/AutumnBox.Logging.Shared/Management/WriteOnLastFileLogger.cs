/*

* ==============================================================================
*
* Filename: WriteOnLastFileLogger
* Description: 
*
* Version: 1.0
* Created: 2020/5/22 14:52:00
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.Logging.Management
{
    public class WriteOnLastFileLogger : CoreLoggerBase
    {
        private readonly ConcurrentQueue<string> lines = new ConcurrentQueue<string>();
        private readonly FileStream fs;

        public WriteOnLastFileLogger(FileStream fs)
        {
            if (fs is null)
            {
                throw new ArgumentNullException(nameof(fs));
            }

            this.fs = fs;
        }
        public override void Log(ILog log)
        {
            lines.Enqueue(log.ToFormatedString());
        }
        private void WriteAllToFile()
        {
            if (!fs.CanWrite) return;
            using var sw = new StreamWriter(fs);
            string text = string.Join(Environment.NewLine, lines.ToArray());
            sw.WriteLine(text);
            sw.Flush();
        }
        protected override void Dispose(bool disposing)
        {
            Log(new Internal.Log("Info", nameof(WriteOnLastFileLogger), "Ended"));
            WriteAllToFile();
            if (disposing)
            {
                fs.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
