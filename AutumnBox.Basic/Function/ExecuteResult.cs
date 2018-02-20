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
    public enum ResultLevel
    {
        Successful = 0,
        MaybeSuccessful = 1,
        MaybeUnsuccessful = 2,
        Unsuccessful = 3,
    }
    public sealed class ExecuteResult
    {
        /// <summary>
        /// 具体输出
        /// </summary>
        public Output OutputData { get; private set; }
        /// <summary>
        /// 是否是强制停止的
        /// </summary>
        public bool WasForcblyStop { get; internal set; } = false;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 判断是否成功
        /// </summary>
        public ResultLevel Level { get; set; } = ResultLevel.Successful;
        public CheckResult CheckResult { get; set; } = CheckResult.OK;
        /// <summary>
        /// 建议信息,一般交由界面进行设置
        /// </summary>
        public string Advise { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="o"></param>
        public ExecuteResult(Output o)
        {
            this.OutputData = o;
        }
    }
}
