/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/19 17:27:18 (UTC +8:00)
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
    public class GeekMemoryCleanerActivator : FunctionFlow
    {
        public const string AppPackageName = "com.ifreedomer.fuckmemory";
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            var result = new NetDebuggingOpener().Run(new NetDebuggingOpenerArgs()
            {
                Port = 5555,
                DevBasicInfo = toolKit.Args.DevBasicInfo
            });
            return result.OutputData;
        }
    }
}
