using AutumnBox.Basic.Data;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 标准的命令结果格式
    /// </summary>
    public interface ICommandResult
    {
        /// <summary>
        /// 返回码
        /// </summary>
        int ExitCode { get; }
        /// <summary>
        /// 输出内容
        /// </summary>
        Output Output { get; }
    }
}
