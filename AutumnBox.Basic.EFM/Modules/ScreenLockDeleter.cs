/* =============================================================================*\
*
* Filename: ScreenLockDeleter.cs
* Description: 
*
* Version: 1.0
* Created: 9/22/2017 03:13:12(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using System.Threading;

namespace AutumnBox.Basic.Function.Modules
{
    public sealed class ScreenLockDeleter : FunctionModule
    {
        protected override OutputData MainMethod()
        {
            AndroidShell _shell = new AndroidShell(Args.DeviceBasicInfo.Id);
            OutputData output = new OutputData
            {
                OutSender = _shell
            };
            _shell.Connect();
            _shell.Switch2Su();
            _shell.SafetyInput("rm /data/system/gesture.key");
            _shell.SafetyInput("adb shell rm /data/system/password.key");
            new Thread(_shell.Disconnect).Start();
            Ae("reboot");
            return output;
        }
    }
}
