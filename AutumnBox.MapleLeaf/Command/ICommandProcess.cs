/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 2:18:44 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Command
{
    public interface ICommandProcess : IDisposable
    {
        IProcessResult Execute();
    }
}
