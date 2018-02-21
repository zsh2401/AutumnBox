/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 23:40:29 (UTC +8:00)
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
    public class DeviceUserDeleter : FunctionFlow
    {
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            AndroidShellV2 shell = new AndroidShellV2(toolKit.Args.Serial);
            var result = shell.Execute("");
            throw new NotImplementedException();
        }
    }
}
