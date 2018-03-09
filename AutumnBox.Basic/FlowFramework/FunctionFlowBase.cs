/* =============================================================================*\
*
* Filename: FunctionFlowBase
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 20:45:39 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// 所有FunctionFlow的基类
    /// </summary>
    public abstract class FunctionFlowBase : object
    {
        /// <summary>
        /// 任何的FunctionFlow执行完都会调用
        /// 调用条件 flow.MustTriggerAnyFinished or (flow.Finished ==null || flow._isSync)
        /// </summary>
        public static event FinishedEventHandler<FlowResult> AnyFinished;
        /// <summary>
        /// 触发"任何一个功能流程完成"的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected static void RisesAnyFinishedEvent(object sender, FinishedEventArgs<FlowResult> e) {
            AnyFinished?.Invoke(sender, e);
        }
    }
}
