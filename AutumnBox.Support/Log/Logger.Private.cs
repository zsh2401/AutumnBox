/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/24 20:12:08 (UTC +8:00)
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
        public static string MakeText(object sender, object prefix, object message)
        {
            string tag = sender is string ? sender.ToString() : sender.GetType().Name;
            string full = $"[{DateTime.Now.ToString("yy-MM-dd HH:mm:ss")}][{tag}/{prefix}]: {message}";
            return full;
        }
    }
}
