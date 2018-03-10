/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 18:15:30 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;

namespace AutumnBox.Basic.Flows
{
    public class RecoveryFlasherArgs : FlowArgs
    {
        public string RecoveryFilePath { get; set; }
    }
    public class RecoveryFlasher : FunctionFlow<RecoveryFlasherArgs>
    {
        protected override Output MainMethod(ToolKit<RecoveryFlasherArgs> toolKit)
        {
            var builder = new OutputBuilder();
            builder.Register(toolKit.Executer);
            toolKit.Fe($"flash recovery \"{toolKit.Args.RecoveryFilePath}\"");
            toolKit.Fe($"boot \"{toolKit.Args.RecoveryFilePath}\"");
            return builder.Result;
        }
    }
}
