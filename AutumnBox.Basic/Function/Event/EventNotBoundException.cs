/* =============================================================================*\
*
* Filename: EventNotBoundException
* Description: 
*
* Version: 1.0
* Created: 2017/10/26 20:12:50 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;

namespace AutumnBox.Basic.Function.Event
{
    [Serializable]
    public class EventNotBoundException : Exception
    {
        public EventNotBoundException()
        {
        }
        public EventNotBoundException(string message) : base(message) { }
    }
}
