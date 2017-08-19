using AutumnBox.Basic.Arg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{
    public interface ICanReturnThreadFunction
    {
        Thread Run(IArgs args);
    }
}
