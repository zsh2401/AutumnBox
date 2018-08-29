/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:29:07 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Data
{
    public interface INotifyOutput
    {
        event OutputReceivedEventHandler OutputReceived;
    }
}
