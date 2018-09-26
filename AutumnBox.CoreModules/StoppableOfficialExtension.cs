/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 2:21:31 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.OpenFramework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules
{
    internal abstract class StoppableOfficialExtension : OfficialVisualExtension
    {
        protected CommandStation CmdStation { get; private set; } = new CommandStation();
        protected override bool VisualStop()
        {
            CmdStation.Free();
            return true;
        }
    }
}
