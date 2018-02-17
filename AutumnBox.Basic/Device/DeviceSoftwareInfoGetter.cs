/************************
** auth： zsh2401@163.com
** date:  2017/1/15 00:45:48
** desc： ...
************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    public class DeviceSoftwareInfoGetter
    {
        private readonly static CommandExecuter executer;
        static DeviceSoftwareInfoGetter()
        {
            executer = new CommandExecuter();
        }
        private readonly DeviceSerial serial;
        public DeviceSoftwareInfoGetter(DeviceSerial serial)
        {
            this.serial = serial;
        }
        private const string ipPattern = @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})";

        public bool IsRootEnable()
        {
            return executer.QuicklyShell(serial,"su -c ls").ExitCode == 0;
        }
        public IPAddress GetLocationIP()
        {
            var result = executer.QuicklyShell(serial, "ifconfig wlan0");
            var match = Regex.Match(result.Output.ToString(), ipPattern);
            if (match.Success)
            {
                return IPAddress.Parse(match.Result("${ip}"));
            }
            else
            {
                result = executer.QuicklyShell(serial, "ifconfig netcfg");
                match = Regex.Match(result.Output.ToString(), ipPattern);
                if (match.Success)
                {
                    return IPAddress.Parse(match.Result("${ip}"));
                }
            }
            return null;
        }
        public bool? IsInstall(string packageName)
        {
            try
            {
                var result = executer.QuicklyShell(serial, $"pm path {packageName}");
                return result.IsSuccessful;
            }
            catch (Exception e)
            {
                Logger.T($"Check {packageName} install status failed", e);
                return null;
            }
        }
    }
}
