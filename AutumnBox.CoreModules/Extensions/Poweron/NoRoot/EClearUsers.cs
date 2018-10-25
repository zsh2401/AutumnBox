using AutumnBox.CoreModules.Aspect;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("Clear all users", "zh-CN:暴力清空所有用户")]
    [ExtDesc("Use the tech by web1n", "zh-CN:使用web1n提供的黑科技暴力清空用户，这将会导致你的应用双开失效，以及其他可能的负面效果")]
    [UserAgree("EClearUsersWarning")]
    [ExtIcon("Icons.nuclear.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    class EClearUsers : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            var gp = new GodPower(this, TargetDevice)
            {
                CmdStation = this.CmdStation
            };
            Progress = 20;
            gp.Extract();
            Progress = 50;
            gp.GetPushCommand()
                .To(OutputPrinter)
                .Execute();
            Progress = 80;
            var result = gp.GetRemoveUserCommand()
                 .To(OutputPrinter)
                 .Execute();
            Progress = 100;
            WriteExitCode(result.ExitCode);
            return result.ExitCode;
        }
    }
}
