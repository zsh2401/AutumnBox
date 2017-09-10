using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{
    public interface IFunctionCanStop
    {
        int CmdProcessPID { get; }
    }
}
