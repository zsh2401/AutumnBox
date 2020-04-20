using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    public static class ABADBManager
    {
        public static ICommandDriver AdbCommandDriver { get; set; }
        public static ICommandDriver FastbootCommandDriver { get; set; }
        public static ICommandDriver CmdCommandDriver { get; set; }
    }
}
