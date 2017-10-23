using AutumnBox.Basic.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;

namespace AutumnBox.ConsoleTester.ObjTest
{
    public class TestModuleTest : FunctionModule
    {
        protected override void HandlingModuleArgs(ModuleArgs e)
        {
            base.HandlingModuleArgs(e);
            Console.WriteLine(e is FileArgs);
        }
        protected override OutputData MainMethod()
        {
            Console.WriteLine("Execute Main Method");
            return new OutputData();
        }
    }
}
