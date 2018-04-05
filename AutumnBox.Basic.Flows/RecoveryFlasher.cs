/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 18:15:30 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// REC刷入器
    /// </summary>
    public class RecoveryFlasherArgs : FlowArgs
    {
        /// <summary>
        /// REC路径
        /// </summary>
        public string RecoveryFilePath { get; set; }
    }
    /// <summary>
    /// REC刷入器(Fastboot下,无需ROOT)
    /// </summary>
    public class RecoveryFlasher : FunctionFlow<RecoveryFlasherArgs>
    {
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
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
