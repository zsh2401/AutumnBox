/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/24 18:28:07 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Cmd;
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    public interface IProcCommandStation : IDisposable
    {
        AdbCommand GetAdbCommand(string cmd);
        AdbCommand GetAdbCommand(IDevice dev, string cmd);
        FastbootCommand GetFastbootCommand();
        FastbootCommand GetFastbootCommand(IDevice dev, string cmd);
        WindowsCmdCommand GetCmdCommand();
        void DisposeAllCommand();
    }
}
