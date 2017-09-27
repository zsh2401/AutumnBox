namespace AutumnBox.Basic.Executer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public partial class CommandExecuter
    {
        /// <summary>
        /// 启动adb服务
        /// </summary>
        public static void Start()
        {
            new CommandExecuter().Execute(new Command("start-server"));
        }
        /// <summary>
        /// 关闭adb服务
        /// </summary>
        public static void Kill()
        {
            new CommandExecuter().Execute(new Command("kill-server"));
        }
        /// <summary>
        /// 重启adb服务
        /// </summary>
        public static void Restart()
        {
            Kill();
            Start();
        }
    }
}
