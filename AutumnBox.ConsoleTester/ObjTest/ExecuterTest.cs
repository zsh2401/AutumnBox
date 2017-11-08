/* =============================================================================*\
*
* Filename: ExecuterTest
* Description: 
*
* Version: 1.0
* Created: 2017/11/8 1:26:33 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.ConsoleTester.ObjTest
{
    public class ExecuterTest
    {
        public static void Start()
        {
            var executer = new Basic.Executer.CExecuter();
            executer.OutputReceived += (s, e) =>
            {
                Console.WriteLine(e.Text);
            };
            //Console.WriteLine(new ABProcess().RunToExited("cmd.exe","/c help").All);
            executer.AdbExecute("help");
            executer.AdbExecute("devices");
            //executer.AdbExecute("version");
        }
    }
}
