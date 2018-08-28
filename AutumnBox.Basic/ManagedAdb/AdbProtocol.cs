/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/28 23:38:16 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ManagedAdb
{
    public static class AdbProtocol
    {
        public const ushort PORT = 5037;
        public const string ADB_EXECUTABLE_FILE_PATH = "google/platform-tools/adb.exe";
        public const string FASTBOOT_EXECUTABLE_FILE_PATH = "google/platform-tools/fastboot.exe";
    }
}
