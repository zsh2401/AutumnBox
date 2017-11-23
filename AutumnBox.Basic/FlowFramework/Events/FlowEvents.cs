/* =============================================================================*\
*
* Filename: FlowEvents
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 15:15:10 (UTC+8:00)
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

namespace AutumnBox.Basic.FlowFramework.Events
{
    public delegate void FinishedEventHandler(object sender,FinishedEventArgs e);
    public class FinishedEventArgs {
        OutputData o;
    }
    public delegate void StartupEventHandler(object sender, StartupEventArgs e);
    public class StartupEventArgs { }
}
