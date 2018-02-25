/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/15 18:37:13 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    public static class PackageHelper
    {
        private static readonly string packagesPattern = @"package:(?<package>.+)";
        /// <summary>
        /// 获取设备上的所有包
        /// </summary>
        /// <param name="devSerial"></param>
        /// <returns></returns>
        public static List<PackageInfo> GetPackages(DeviceSerial devSerial)
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
        public static bool? IsInstall(DeviceSerial devSerial, string packageName)
        {
            var result = PackageManagerShared.Executer.QuicklyShell(devSerial, $"pm path {packageName}");
            return result.IsSuccessful;
        }
    }
}
