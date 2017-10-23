/* =============================================================================*\
*
* Filename: Exceptions.cs
* Description: 
*
* Version: 1.0
* Created: 9/3/2017 15:50:40(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

using System;

namespace AutumnBox.Basic.Util
{
    [Serializable]
    public class EventAddException : Exception
    {
        public EventAddException() { }
        public EventAddException(string message) : base(message) { }
    }
    [Serializable]
    public class EventNotBoundException : Exception {
        public EventNotBoundException() { }
        public EventNotBoundException(string message):base(message) {}
    }
    [Serializable]
    public class ArgErrorException : Exception { }
    [Serializable]
    public class DeviceNotFoundException : Exception { }
    [Serializable]
    public class FuncNotSupportStopException : Exception { }
}
