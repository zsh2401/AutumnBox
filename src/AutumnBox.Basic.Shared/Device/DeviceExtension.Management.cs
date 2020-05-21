/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:12:14 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Util;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device
{
    partial class DeviceExtension
    {
        /// <summary>
        /// 获取build.prop中的值
        /// </summary>
        /// <param name="device"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetProp(this IDevice device, string key)
        {
            var result = device.Shell($"getprop {key}").ThrowIfExitCodeNotEqualsZero();
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

        static readonly Regex ipRegex = new Regex(@"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})", RegexOptions.Compiled);
        /// <summary>
        /// 获取该设备在局域网中的IP
        /// </summary>
        /// <param name="device"></param>
        /// <exception cref="System.Exception">无法正确获取数据</exception>
        /// <returns></returns>
        public static System.Net.IPAddress GetLanIP(this IDevice device)
        {
            var result = device.Shell("ifconfig wlan0");
            AutumnBox.Logging.SLogger.Info(nameof(DeviceExtension), result.Output);
            var match = ipRegex.Match(result.Output.ToString());
            if (match.Success)
            {
                return System.Net.IPAddress.Parse(match.Result("${ip}"));
            }
            else
            {
                result = device.Shell("ifconfig netcfg");
                match = ipRegex.Match(result.Output.ToString());
                if (match.Success)
                {
                    return System.Net.IPAddress.Parse(match.Result("${ip}"));
                }
            }
            throw new System.Exception("can not get lan ip address");
        }
        /// <summary>
        /// 检查是否有SU权限
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static bool HaveSU(this IDevice device)
        {
            ///话说是哪位鬼才想到的使用不验证 su 存在直接用 su -c 检查有没有 root 的？
            ///var command = new SuCommand(device, "ls");


            /// 这玩意太长，我是不满意
            /// 顺便在控制台执行的时候遇到了一些神奇的问题
            /// 话说啥时候 ShellCommand 负责解释的是 sh 而不是 adb -s <device> shell <cmd> 呢？
            return device.Shell("echo id | su | grep uid=0 >/dev/null").ExitCode == 0;
        }
    }
}
