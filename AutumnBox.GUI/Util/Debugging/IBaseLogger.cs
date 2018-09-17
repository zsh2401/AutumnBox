/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/17 16:45:35 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Debugging
{
    internal interface IBaseLogger
    {
        event EventHandler<LogEventArgs> Logging;

        void WriteLine(object content,int logLevel);
    }
}
