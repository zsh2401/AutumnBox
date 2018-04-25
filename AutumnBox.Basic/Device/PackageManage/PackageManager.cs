/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/27 10:45:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using AutumnBox.Support.Log;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.PackageManage
{
    /// <summary>
    /// 包管理器
    /// </summary>
    public static class PackageManager
    {
        private const string TAG = "PackageManager";
        /// <summary>
        /// 卸载App
        /// </summary>
        /// <param name="device"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public static bool UninstallApp(DeviceSerialNumber device, string packageName) {
            var exeResult = PackageManagerShared.Executer.Execute(Command.MakeForAdb(device, "uninstall " + packageName));
            return !exeResult.Contains("Failure");
        }
        /// <summary>
        /// 清空app数据
        /// </summary>
        /// <param name="device"></param>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public static bool CleanAppData(DeviceSerialNumber device, string packageName) {
            var exeResult = PackageManagerShared.Executer.QuicklyShell(device, "pm clear " + packageName);
            Logger.Info(TAG,$"clean {packageName} data success?{exeResult.IsSuccessful}");
            return exeResult.IsSuccessful;
        }
        private static readonly string packagesPattern = @"package:(?<package>.+)";
        /// <summary>
        /// 获取设备上的所有包
        /// </summary>
        /// <param name="devSerial"></param>
        /// <returns></returns>
        public static List<PackageInfo> GetPackages(DeviceSerialNumber devSerial)
        {
            var result = PackageManagerShared.Executer.QuicklyShell(devSerial, $"pm list packages");
            var matches = Regex.Matches(result.ToString(), packagesPattern);
            List<PackageInfo> packages = new List<PackageInfo>();
            foreach (Match m in matches)
            {
                packages.Add(new PackageInfo(devSerial, m.Result("${package}")));
            }
            return packages;
        }
        /// <summary>
        /// 检查是否安装了某个APP
        /// </summary>
        /// <param name="devSerial">设备</param>
        /// <param name="packageName">应用名</param>
        /// <returns></returns>
        public static bool? IsInstall(DeviceSerialNumber devSerial, string packageName)
        {
            var result = PackageManagerShared.Executer.QuicklyShell(devSerial, $"pm path {packageName}");
            return result.IsSuccessful;
        }
    }
}
