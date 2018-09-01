/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 16:13:14 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling.Cmd
{
    public class WindowsCmdCommand : ProcessBasedCommand
    {
        public WindowsCmdCommand(string args) : base("cmd.exe", "/c " + args)
        {
        }
    }
}
