using AutumnBox.Basic.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Executer;

namespace Tester
{
    public class TestFunction : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            return Executer.ExecuteWithoutDevice("help");
        }
    }
}
