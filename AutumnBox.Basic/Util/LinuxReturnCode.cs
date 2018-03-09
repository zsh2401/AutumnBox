/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/29 20:46:08
** filename: ShellReturnCode.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// Linux执行返回码
    /// </summary>
    public enum LinuxReturnCode
    {
        /// <summary>
        /// 没事
        /// </summary>
        None = 0,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 1,
        /// <summary>
        /// KeyHasExpired
        /// </summary>
        KeyHasExpired = 127,
        /// <summary>
        /// 未知
        /// </summary>
        Unknow = -1,
    }
}
