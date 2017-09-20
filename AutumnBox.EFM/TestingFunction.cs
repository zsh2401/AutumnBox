using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Functions
{
    public class TestingFunction : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            OutputData output = executer.ExecuteWithoutDevice("fuck");
            return output;
        }
    }
}
