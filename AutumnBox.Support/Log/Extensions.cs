/*************************************************
** auth： zsh2401@163.com
** date:  2018/5/23 19:57:42 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Support.Log
{
    public static class Extensions
    {
        public static void LogWarn(this Exception ex, object sender, string msg = "exception")
        {
            Logger.Warn(sender, msg, ex);
        }
    }
}
