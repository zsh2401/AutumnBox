/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:51:24 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Debugging
{
    public class Logger<TSenderClass>
    {
        private readonly string TAG;
        public Logger()
        {
            TAG = typeof(TSenderClass).Name;
        }
        public void Debug(object content)
        {
            Logger.Debug(TAG, content);
        }
        public void Debug(string method, object content)
        {
            Logger.Debug(TAG + $".{method}", content);
        }
        public void Info(object content)
        {
            Logger.Info(TAG, content);
        }

    }
}
