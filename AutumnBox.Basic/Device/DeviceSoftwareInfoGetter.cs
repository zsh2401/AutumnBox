/************************
** auth： zsh2401@163.com
** date:  2017/1/15 00:45:48
** desc： ...
************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Support.Log;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备软件信息获取器
    /// </summary>
    public class DeviceSoftwareInfoGetter
    {
        private readonly static CommandExecuter executer;
        static DeviceSoftwareInfoGetter()
        {
            executer = new CommandExecuter();
        }
        private readonly DeviceSerialNumber serial;
        /// <summary>
        /// 创建DeviceSoftwareInfoGetter的新实例
        /// </summary>
        /// <param name="serial"></param>
        public DeviceSoftwareInfoGetter(DeviceSerialNumber serial)
        {
            this.serial = serial;
        }
        private const string ipPattern = @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})";
        /// <summary>
        /// 检测ROOT是否可以使用
        /// </summary>
        /// <returns></returns>
        public bool IsRootEnable()
        {
            return executer.QuicklyShell(serial,"su -c ls").GetExitCode() == 0;
        }
        /// <summary>
        /// 获取设备的局域网IP
        /// </summary>
        /// <returns></returns>
        public IPAddress GetLocationIP()
        {
            var result = executer.QuicklyShell(serial, "ifconfig wlan0");
            var match = Regex.Match(result.ToString(), ipPattern);
            if (match.Success)
            {
                return IPAddress.Parse(match.Result("${ip}"));
            }
            else
            {
                result = executer.QuicklyShell(serial, "ifconfig netcfg");
                match = Regex.Match(result.ToString(), ipPattern);
                if (match.Success)
                {
                    return IPAddress.Parse(match.Result("${ip}"));
                }
            }
            return null;
        }
        /// <summary>
        /// 检测设备是否安装某个应用
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public bool? IsInstall(string packageName)
        {
            try
            {
                var result = executer.QuicklyShell(serial, $"pm path {packageName}");
                return result.IsSuccessful;
            }
            catch (Exception e)
            {
                Logger.Warn($"Check {packageName} install status failed", e);
                return null;
            }
        }
    }
}
