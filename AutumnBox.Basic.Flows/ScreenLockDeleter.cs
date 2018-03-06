/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 18:07:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class ScreenLockDeleter : FunctionFlow
    {
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            var builder = new OutputBuilder();
            var shell = new AndroidShellV2(toolKit.Args.Serial);
            builder.Register(shell);
            builder.Register(toolKit.Executer);
            shell.Execute("rm /data/system/gesture.key", AndroidShellV2.LinuxUser.Su);
            shell.Execute("rm /data/system/password.key", AndroidShellV2.LinuxUser.Su);
            toolKit.Ae("reboot");
            return builder.Result;
        }
    }
}
