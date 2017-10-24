/* =============================================================================*\
*
* Filename: Executer
* Description: 
*
* Version: 1.0
* Created: 2017/10/24 15:18:06 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public class Executer
    {
        
        private static Object _Locker = new object();
        private static Process CoreProcess;
        static Executer() {

        }
        OutputData Execute(string fileName, string args) {
            throw new NotImplementedException();
        }
    }
}
