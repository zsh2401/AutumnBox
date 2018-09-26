/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/22 23:06:30 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    public class ExtensionArgs : EventArgs
    {
        public IExtensionProcess CurrentProcess { get; set; }
        public IExtensionWrapper Wrapper { get; set; }
        public IDevice TargetDevice { get; set; }
    }
    public class ExtensionDestoryArgs : EventArgs
    {

    }
    public class ExtensionStopArgs : EventArgs {

    }
    public class ExtensionFinishedArgs : EventArgs
    {
        public int ExitCode { get; set; }
    }
}
