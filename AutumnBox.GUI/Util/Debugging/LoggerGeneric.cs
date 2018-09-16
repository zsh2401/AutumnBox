/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:51:24 (UTC +8:00)
** desc： ...
*************************************************/
namespace AutumnBox.GUI.Util.Debugging
{
    internal class Logger<TSenderClass> : Logger
    {
        public Logger() : base(typeof(TSenderClass).Name)
        {
        }
    }
}
