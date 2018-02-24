/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 21:21:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.OpenApi
{
    public static class Logger
    {
        public static void Log(string tag,string message) {
            string msg = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][{tag}/Mod]{message}";
            Trace.WriteLine(msg);
            Console.WriteLine(msg);
        }
    }
}
