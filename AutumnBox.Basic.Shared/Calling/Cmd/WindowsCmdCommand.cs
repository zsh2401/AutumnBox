/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 16:13:14 (UTC +8:00)
** desc： ...
*************************************************/

namespace AutumnBox.Basic.Calling.Cmd
{
    /// <summary>
    /// Windows cmd命令
    /// </summary>
    public class WindowsCmdCommand : ProcessBasedCommand
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="args"></param>
        public WindowsCmdCommand(string args) : base("cmd.exe", "/c " + args)
        {
        }
    }
}
