/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 17:46:35 (UTC +8:00)
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
    public class OemRelocker : FunctionFlow
    {
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            return toolKit.Fe("oem lock");
        }
    }
}
