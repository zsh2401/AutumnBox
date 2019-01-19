/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/

using System.Collections.Generic;
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("Clear all users", "zh-CN:暴力清空所有用户")]
    [ExtDesc("Use the tech by web1n", "zh-CN:使用web1n提供的黑科技暴力清空用户，这将会导致你的应用双开失效，以及其他可能的负面效果")]
    [UserAgree("EClearUsersWarning")]
    [ExtIcon("Icons.nuclear.png")]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    class EClearUsers : OfficialVisualExtension
    {
        protected override int VisualMain()
        {
            using (var executor = new CommandExecutor())
            {
                var dpm = new CstmDpmCommander(executor, this, TargetDevice);
                Progress = 20;
                dpm.Extract();
                Progress = 50;
                dpm.PushToDevice();
                Progress = 80;
                try
                {
                    dpm.RemoveUsers();
                    WriteExitCode(0);
                    return 0;
                }
                catch (CommandErrorException ex)
                {
                    WriteExitCode(ex.ExitCode ?? 1);
                    return ex.ExitCode ?? 1;
                }
                finally
                {
                    Progress = 100;
                }
            }
        }
        protected override void OnDestory(object args)
        {
            if (Args.CurrentThread.ExitCode == OK)
            {
                DeviceSelectedOnCreating.Reboot2System();
            }
            base.OnDestory(args);
        }
    }
}
