/* =============================================================================*\
*
* Filename: CommandExecuter.Static.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 21:23:33(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
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
