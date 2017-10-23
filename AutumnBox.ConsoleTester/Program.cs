using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var o = new FunctionModuleProxy<ObjTest.TestModuleTest>(new FileArgs());
        }
    }
}
