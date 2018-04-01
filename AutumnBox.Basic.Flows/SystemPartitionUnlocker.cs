/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 17:51:41 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;
using System.Threading;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// 系统分区解锁期
    /// </summary>
    public class SystemPartitionUnlocker : FunctionFlow
    {
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            toolKit.Ae("root");
            Thread.Sleep(300);
           return toolKit.Ae("disable-verity");
        }
    }
}
