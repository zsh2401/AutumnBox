/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:41:37 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 包管理器实现
    /// </summary>
    public class PackageManager : DependOnDeviceObject, IPackageManager
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public PackageManager(IDevice device) : base(device)
        {
        }

        private const string packagesPattern = @"package:(?<package>.+)";
        /// <summary>
        /// 获取设备上所有的包
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PackageInfo> GetPackages()
        {
            Output result = Device.Shell($"pm list packages").Item1;
            var matches = Regex.Matches(result.ToString(), packagesPattern);
            List<PackageInfo> packages = new List<PackageInfo>();
            foreach (Match m in matches)
            {
                packages.Add(new PackageInfo(Device, m.Result("${package}"),false));
            }
            return packages;
        }

        /// <summary>
        /// 从PC端安装APK
        /// </summary>
        /// <param name="file"></param>
        public void Install(FileInfo file)
        {
            Device.Adb($"install \"{file.FullName}\"")
                .ThrowIfExitCodeNotEqualsZero();
        }

        /// <summary>
        /// 判断设备是否安装某个包名的应用
        /// </summary>
        /// <param name="pkgName"></param>
        /// <returns></returns>
        public bool IsInstall(string pkgName)
        {
            var result = Device.Shell($"pm path {pkgName}");
            return result.Item2 == 0;
        }

        /// <summary>
        /// 已安装的包路径
        /// </summary>
        /// <param name="pkgName"></param>
        /// <returns></returns>
        public string Path(string pkgName)
        {
            return Device.Shell($"pm path").Item1.ToString();
        }

        /// <summary>
        /// 获取包信息
        /// </summary>
        /// <param name="pkgName"></param>
        /// <returns></returns>
        public PackageInfo PkgOf(string pkgName)
        {
            return new PackageInfo(Device, pkgName);
        }

        /// <summary>
        /// 卸载某个包名的应用
        /// </summary>
        /// <param name="pkgName"></param>
        public void Uninstall(string pkgName)
        {
            Device.Adb($"uninstall {pkgName}")
                 .ThrowIfExitCodeNotEqualsZero();
        }
    }
}
