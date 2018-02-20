using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
   public interface IShellOutput:IPrintable
    {
        int ExitCode { get; }
        bool IsSuccessful { get; }

        Output ToOutputData();
    }
}
