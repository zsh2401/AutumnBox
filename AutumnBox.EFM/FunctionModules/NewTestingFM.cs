using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Functions.FunctionModules
{
    public class NewTestingFM : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            OutputData o = new OutputData() { OutSender = Executer};
            Executer.AdbExecute("help");
            Executer.AdbExecute("devices");
            return o;
        }
    }
}
