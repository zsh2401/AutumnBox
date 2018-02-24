/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/24 20:25:13 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester
{
    public class LogWriterTraceListener : TraceListener
    {

        private readonly StreamWriter logWriter;

        public LogWriterTraceListener(string fileName)
        {
            //logWriter = new StreamWriter(fileName, true)
            //{
            //    AutoFlush = true
            //};
        }

        public override void Write(string message)
        {
            //logWriter.Write(message);
            Debugger.Log(0, "fucker", message);
        }

        public override void WriteLine(string message)
        {
            //logWriter.WriteLine(message);
            Debugger.Log(0, "fucker", message);
        }

        private static string MakeText(string categroy, string prefix, string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{DateTime.Now.ToString("yyyy-MM-dd")}]");
            sb.Append($"[{categroy}/{prefix}]");
            sb.Append($": {message}");
            return sb.ToString();
        }
    }
}
