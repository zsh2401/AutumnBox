/* =============================================================================*\
*
* Filename: NewTestingFM.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 20:05:18(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
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
