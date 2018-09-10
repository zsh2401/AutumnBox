/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/17 21:00:19 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Data;

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// 去除了所有泛型特征的FunctionFlow接口
    /// </summary>
    public interface IFunctionFlowBase : ICompletable,INotifyOutput
    {
        /// <summary>
        ///是否已关闭
        /// </summary>
        bool IsClosed { get; }
        /// <summary>
        /// 是否必须触发AnyFinished事件
        /// </summary>
        bool MustTiggerAnyFinishedEvent { set; }
        /// <summary>
        /// 异步运行
        /// </summary>
        void RunAsync();
    }
}
