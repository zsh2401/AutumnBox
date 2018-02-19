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
    public static class Loader
    {
        public static void Load() {
            AdbHelper.StartServer();
        }
    }
}
