/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:29:25 (UTC +8:00)
** desc： ...
*************************************************/
using System;

namespace AutumnBox.Basic.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string cmd) : base(cmd + "not found")
        {

        }
    }
}
