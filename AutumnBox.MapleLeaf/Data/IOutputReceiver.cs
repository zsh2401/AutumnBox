/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 22:58:43 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.Data
{
    public interface IOutputReceiver
    {
        void OnOutputReceived(object sender, OutputReceivedEventArgs e);
    }
}
