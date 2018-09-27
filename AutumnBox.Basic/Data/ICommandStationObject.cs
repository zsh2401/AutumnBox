using AutumnBox.Basic.Calling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Data
{
    interface ICommandStationObject : INotifyOutput
    {
        CommandStation CmdStation { get; set; }
    }
}
