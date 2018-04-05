/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 17:46:35 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.FlowFramework;

namespace AutumnBox.Basic.Flows
{
    /// <summary>
    /// OEM锁定器(BL加锁器)
    /// </summary>
    public class OemRelocker : FunctionFlow
    {
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="toolKit"></param>
        /// <returns></returns>
        protected override Output MainMethod(ToolKit<FlowArgs> toolKit)
        {
            return toolKit.Fe("oem lock");
        }
    }
}
