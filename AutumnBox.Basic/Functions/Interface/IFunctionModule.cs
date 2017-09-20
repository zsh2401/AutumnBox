using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions.Interface
{
    interface IFunctionModule
    {
        void Run();
    }
    interface ICustomFunctionModule
    {
        event StartEventHandler Started;
        event FinishEventHandler Finished;
        CommandExecuter executer { get; }
        string DeviceID { get; set; }
        OutputData Run();
    }
}
