/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:12:14 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device
{
    partial class DeviceExtension
    {
        private const string ipPattern = @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})";
        /// <summary>
        /// 获取build.prop中的值
        /// </summary>
        /// <param name="device"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetProp(this IDevice device, string key)
        {
           var result =  new ShellCommand(device, $"getprop {key}")
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
            return result.Output.ToString().Trim();
        }
        /// <summary>
        /// Pull文件
        /// </summary>
        /// <param name="device"></param>
        /// <param name="fileOnDevice"></param>
        /// <param name="savePath"></param>
        public static void Pull(this IDevice device, string fileOnDevice, string savePath)
        {
            device.Adb($"pull {fileOnDevice} {savePath}")
                .ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 获取该设备在局域网中的IP
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static System.Net.IPAddress GetLanIP(this IDevice device)
        {
            var result = device.Shell("ifconfig wlan0");
            var match = System.Text.RegularExpressions.Regex.Match(result.ToString(), ipPattern);
            if (match.Success)
            {
                return System.Net.IPAddress.Parse(match.Result("${ip}"));
            }
            else
            {
                result = device.Shell("ifconfig netcfg");
                match = System.Text.RegularExpressions.Regex.Match(result.ToString(), ipPattern);
                if (match.Success)
                {
                    return System.Net.IPAddress.Parse(match.Result("${ip}"));
                }
            }
            return null;
        }
        /// <summary>
        /// 检查是否有SU权限
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static bool HaveSU(this IDevice device)
        {
            var command = new SuCommand(device, "ls");
            return command.Execute().ExitCode == 0;
        }
        /// <summary>
        /// 获取Shell命令对象
        /// </summary>
        /// <param name="device"></param>
        /// <param name="sh"></param>
        /// <returns></returns>
        public static ProcessBasedCommand GetShellCommand(this IDevice device, string sh)
        {
            device.ThrowIfNotAlive();
            return new ShellCommand(device, sh);
        }
    }
}
