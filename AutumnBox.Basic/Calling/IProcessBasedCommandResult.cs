using AutumnBox.Basic.Data;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 命令执行结果
    /// </summary>
    public interface IProcessBasedCommandResult
    {
        /// <summary>
        /// 输出
        /// </summary>
        Output Output { get; }
        /// <summary>
        /// 返回码
        /// </summary>
        int ExitCode { get; }
    }
}
