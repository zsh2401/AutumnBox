/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:41:37 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Management.AppFx.Impl
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
        /// <summary>
        /// 从PC端安装APK
        /// </summary>
        /// <param name="file"></param>
        public void Install(FileInfo file)
        {
            new AdbCommand($"install \"{file.FullName}\"").Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }

        /// <summary>
        /// 判断设备是否安装某个包名的应用
        /// </summary>
        /// <param name="pkgName"></param>
        /// <returns></returns>
        public bool IsInstall(string pkgName)
        {
            var result = new AdbCommand($"pm path {pkgName}").Execute();
            return result.ExitCode == 0;
        }

        /// <summary>
        /// 卸载某个包名的应用
        /// </summary>
        /// <param name="pkgName"></param>
        public void Uninstall(string pkgName)
        {
            new AdbCommand($"uninstall {pkgName}").Execute()
                 .ThrowIfExitCodeNotEqualsZero();
        }
    }
}
