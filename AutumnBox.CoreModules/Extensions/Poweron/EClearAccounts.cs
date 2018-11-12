using AutumnBox.Basic.Exceptions;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("Clear all accounts", "zh-CN:暴力清空所有账号")]
    [ExtDesc("Use the tech by web1n", "zh-CN:使用web1n提供的黑科技暴力清空账号")]
    [UserAgree("EClearAccountsWarning")]
    [ExtIcon("Icons.nuclear.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EClearAccounts : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            var dpm = new CstmDpmCommander(this, TargetDevice)
            {
                CmdStation = this.CmdStation
            };
            Progress = 20;
            dpm.Extract();
            Progress = 50;
            dpm.PushToDevice();
            Progress = 80;
            int exitCode = 0;
            try
            {
                dpm.RemoveAccounts();
                WriteExitCode(0);
                return 0;
            }
            catch (CommandErrorException ex)
            {
                WriteExitCode(ex.ExitCode ?? 1);
                exitCode = ex.ExitCode ?? 1;
                return ex.ExitCode ?? 1;
            }
            finally
            {
                Progress = 100;
            }
        }
    }
}
