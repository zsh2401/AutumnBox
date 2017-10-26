/* =============================================================================*\
*
* Filename: RebootOperator.cs
* Description: 
*
* Version: 1.0
* Created: 9/5/2017 18:24:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Event;

namespace AutumnBox.Basic.Function.Modules
{
    /// <summary>
    /// 手机重启器
    /// </summary>
    public class RebootOperator : FunctionModule
    {
        enum ExecuterType
        {
            Adb,
            Fastboot,
        }
        private ExecuterType t;
        private RebootArgs _Args;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this._Args = (e.ModuleArgs as RebootArgs);
            switch (_Args.nowStatus)
            {
                case DeviceStatus.FASTBOOT:
                    t = ExecuterType.Fastboot;
                    break;
                default:
                    t = ExecuterType.Adb;
                    break;
            }
        }
        protected override OutputData MainMethod()
        {
            string command;

            if (_Args.rebootOption == RebootOptions.Bootloader)
            {
                command = "reboot-bootloader";
            }
            else if (_Args.rebootOption == RebootOptions.System)
            {
                command = "reboot";
            }
            else if (_Args.nowStatus != DeviceStatus.FASTBOOT && _Args.rebootOption == RebootOptions.Recovery)
            {
                command = "reboot recovery";
            }
            else
            {
                throw new System.Exception();
            }
            OutputData o;
            switch (t)
            {
                case ExecuterType.Adb:
                    o = Ae(command);
                    break;
                default:
                    o = Fe(command);
                    break;
            }
            return o;
        }
    }
}
