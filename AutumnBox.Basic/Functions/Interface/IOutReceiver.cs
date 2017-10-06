/* =============================================================================*\
*
* Filename: IOutReceiver.cs
* Description: 
*
* Version: 1.0
* Created: 9/25/2017 06:12:42(UTC+8:00)
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

namespace AutumnBox.Basic.Functions.Interface
{
    public interface IOutReceiver
    {
        void OutReceived(object sender,DataReceivedEventArgs e);
        void ErrorReceived(object sender, DataReceivedEventArgs e);
    }
}
