/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:51:24 (UTC +8:00)
** desc： ...
*************************************************/

using System;

namespace AutumnBox.Basic.Util.Debugging
{
    internal class Logger<TSenderClass> : Logger
    {
        private readonly string TAG;
        public Logger():base(typeof(TSenderClass).Name)
        {
            TAG = typeof(TSenderClass).Name;
        }
    }
}
