/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 1:34:42 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    internal class LoggerTraceListener : TraceListener
    {

        public override void Write(object o, string category)
        {
            base.Write(o, category);
        }
        public override void Write(string message)
        {
            Debugger.Log(0,null,message);
        }

        public override void WriteLine(string message)
        {
            Debugger.Log(0, null, message);
        }
    }
}
