/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:03:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Schema;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling.Adb
{
    public class AdbCommandBuilder : IArgBuilder, IDeviceSettableBuilder<IShellBuilder>, IShellBuilder
    {
        private const string ADB_FILE = AdbProtocol.ADB_EXECUTABLE_FILE_PATH;
        private List<string> args = new List<string>();
        public AdbCommandBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            args.Clear();
            Arg("-P");
            Arg(AdbServer.Instance.Port.ToString());
        }

        public void Clear()
        {
            Reset();
        }

        public IArgBuilder Arg(string arg)
        {
            args.Add(arg);
            return this;
        }

        public IShellBuilder Device(string serialNumber)
        {
            serialNumber.ThrowIfNullArg();
            Arg("-s");
            Arg(serialNumber);
            return this;
        }

        public IShellBuilder Device(IDevice device)
        {
            device.ThrowIfNullArg();
            Device(device.SerialNumber);
            return this;
        }

        public IArgBuilder Shell(bool su = false)
        {
            Arg("shell");
            if (su)
            {
                Arg("su");
                Arg("-c");
            }
            return this;
        }

        public IProcessBasedCommand ToCommand()
        {
            return new AdbCommand(ArgsToString());
        }

        public string ArgsToString()
        {
            return string.Join(" ", args);
        }

        public override string ToString()
        {
            return ADB_FILE + " " + args;
        }

        public IArgBuilder ArgWithDoubleQuotation(string arg)
        {
            return Arg("\"" + arg + "\"");
        }

        public static IProcessBasedCommand For(string args)
        {
            return new AdbCommand(args);
        }
    }
}
