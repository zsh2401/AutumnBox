/*
 @zsh2401 2017/9/6
 一些自定义的异常
 */

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
