/* =============================================================================*\
*
* Filename: Stoper
* Description: 
*
* Version: 1.0
* Created: 2017/11/24 17:33:18 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// FunctionFlow终止器
    /// </summary>
    public sealed class Stoper : IForceStoppable
    {
        IForceStoppable obj;
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="obj"></param>
        public Stoper(IForceStoppable obj)
        {
            this.obj = obj;
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void ForceStop()
        {
            obj.ForceStop();
        }
    }
}
