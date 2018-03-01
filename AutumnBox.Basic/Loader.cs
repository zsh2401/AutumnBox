/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/19 16:22:27 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Adb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic
{
    /// <summary>
    /// 加载器
    /// </summary>
    public static class Loader
    {
        /// <summary>
        /// 加载AuutmnBox.Basic
        /// </summary>
        public static void Load() {
            AdbHelper.StartServer();
        }
    }
}
