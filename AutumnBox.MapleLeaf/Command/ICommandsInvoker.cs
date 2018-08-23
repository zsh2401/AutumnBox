/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 19:26:08 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Command
{
    public interface ICommandsInvokerStopper
    {
        void Stop();
    }
    public interface ICommandsInvoker
    {
        IEnumerable<IProcessResult> Execute();
        ICommandsInvokerStopper ExecuteAsync(Action<IEnumerable<IProcessResult>> callback);
    }
}
