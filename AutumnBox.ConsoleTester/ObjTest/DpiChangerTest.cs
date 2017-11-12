/* =============================================================================*\
*
* Filename: DpiChangerTest
* Description: 
*
* Version: 1.0
* Created: 2017/11/12 18:09:20 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester.ObjTest
{
    class DpiChangerTest
    {
        public static void Run()
        {
            FunctionModuleProxy fmp =
                    FunctionModuleProxy.Create<DpiChanger>(new Basic.Function.Args.DpiChangerArgs(Program.mi4) { Dpi = 400 });
            Console.WriteLine(fmp.FastRun().OutputData.All);
        }
    }
}
