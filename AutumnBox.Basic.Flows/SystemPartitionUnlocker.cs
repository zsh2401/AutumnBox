/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 17:51:41 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows
{
    public class SystemPartitionUnlocker : FunctionFlow
    {
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            toolKit.Ae("root");
            Thread.Sleep(300);
           return toolKit.Ae("disable-verity");
        }
    }
}
