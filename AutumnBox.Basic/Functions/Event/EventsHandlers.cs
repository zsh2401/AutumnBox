/* =============================================================================*\
*
* Filename: EventsHandlers.cs
* Description: 
*
* Version: 1.0
* Created: 9/8/2017 16:19:40(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;

namespace AutumnBox.Basic.Functions.Event
{
    public delegate void SingleFileSendedEventHandler(object sender,SingleFileSendedEventArgs e);
    public class SingleFileSendedEventArgs : EventArgs {
        public readonly string FileName;
        public SingleFileSendedEventArgs(string fileName) {
            FileName = fileName;
        }
    }
}
