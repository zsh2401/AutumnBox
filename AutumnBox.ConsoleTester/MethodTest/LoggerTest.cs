/* =============================================================================*\
*
* Filename: LoggerTest
* Description: 
*
* Version: 1.0
* Created: 2017/10/31 3:22:34 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Shared.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester.MethodTest
{
    [LogProperty(TAG = "Testing...",Show =true)]
    public class LoggerTest
    {
        //[LogProperty(TAG = "Test Method",Show =false)]
        public static void Test() {
            Logger.D("Wow!");
        }
    }
}
