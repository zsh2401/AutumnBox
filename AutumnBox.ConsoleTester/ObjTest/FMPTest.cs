/* =============================================================================*\
*
* Filename: FMPTest
* Description: 
*
* Version: 1.0
* Created: 2017/11/5 15:35:13 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester.ObjTest
{
    public class FMPTest
    {
        public static void CreateTest()
        {
            var fmp = FunctionModuleProxy.Create(typeof(BreventServiceActivator), new ModuleArgs(new Basic.Devices.DeviceBasicInfo()));
        }
        public static void PropertyTest() {
            var fmp = FunctionModuleProxy.Create(typeof(BreventServiceActivator), new ModuleArgs(new Basic.Devices.DeviceBasicInfo()));
            Console.WriteLine(fmp.FunctionModuleType);
        }
    }
}
