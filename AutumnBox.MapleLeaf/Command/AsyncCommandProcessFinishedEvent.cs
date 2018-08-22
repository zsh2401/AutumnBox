/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 2:30:02 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Basis
{
    public delegate void AsyncCommandProcessFinishedEventHandler(object sender,AsyncCommandProcessFinishedEventArgs e);
    public class AsyncCommandProcessFinishedEventArgs:EventArgs
    {
    }
}
