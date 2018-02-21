/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/20 16:41:49 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public sealed class SuShell:IDisposable
    {
        public DeviceSerial Device { get; private set; }
        private readonly ProcessStartInfo processStartInfo;
        public event OutputReceivedEventHandler OutputReceived;
        private SuShell(DeviceSerial dev) {

        }
        public AdvanceOutput Execute(string command) {
            StringBuilder sb = new StringBuilder();
            throw new NotImplementedException();
        }
        public static SuShell From(DeviceSerial dev) {
            if (RootCheck(dev)) return new SuShell(dev);
            else throw new DeviceHaveNoRootException(dev);
        }
        private const string exitCodePattern = @"__exitcode__(?<code>\d+)";
        public static bool RootCheck(DeviceSerial dev) {
            var output = CommandExecuter.Static.Execute(
                 Command.MakeForAdb(dev, "shell \"su -c \"ls ; echo __exitcode__$?\"\""));
            var match = Regex.Match(output.ToString(), exitCodePattern, RegexOptions.Multiline);
            return match.Success ? match.Result("${code}") == "0":false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
