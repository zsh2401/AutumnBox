/* =============================================================================*\
*
* Filename: ExecuteResult.cs
* Description: 
*
* Version: 1.0
* Created: 9/25/2017 07:20:51(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Function
{
    using AutumnBox.Basic.Executer;
    public class ExecuteResult
    {
        public enum Type
        {
            Successful,
            Unsuccessful,
            MaybeSuccessful,
            MaybeUnsuccessful,
        }
        /// <summary>
        /// 具体输出
        /// </summary>
        public OutputData OutputData { get; internal set; }
        /// <summary>
        /// 是否是强制停止的
        /// </summary>
        public bool WasForcblyStop { get; internal set; } = false;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 判断是否成功
        /// </summary>
        public Type ResultType { get; set; } = Type.Successful;
        /// <summary>
        /// 建议信息,一般交由界面进行设置
        /// </summary>
        public string Advise { get; set; } = string.Empty;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="o"></param>
        public ExecuteResult(OutputData o)
        {
            this.OutputData = o;
        }
    }
}
