/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 2:21:31 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules
{
    internal abstract class StoppableOfficialExtension : OfficialVisualExtension
    {
        protected bool RequestStop { get; private set; } = false;
        protected CommandStation CmdStation { get; private set; } = new CommandStation();
        protected override bool VisualStop()
        {
            RequestStop = true;
            CmdStation.Free();
            return true;
        }
    }
}
