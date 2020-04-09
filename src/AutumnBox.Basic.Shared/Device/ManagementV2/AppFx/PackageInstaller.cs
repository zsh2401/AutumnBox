using AutumnBox.Basic.Calling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.Basic.Device.ManagementV2.AppFx
{
    /// <summary>
    /// 包安装器
    /// </summary>
    public sealed class PackageInstaller
    {
        private readonly IDevice device;
        private readonly ICommandExecutor executor;
        /// <summary>
        /// 构造一个包安装器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        public PackageInstaller(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
        /// <summary>
        /// 安装位于PC机上的APK文件
        /// </summary>
        /// <returns></returns>
        public ICommandResult Install(FileInfo packageFile,
            bool allowDowngrade = true)
        {
            if (packageFile == null)
            {
                throw new ArgumentNullException(nameof(packageFile));
            }
            string args = allowDowngrade ? "-d" : "";
            return executor.Adb(device, $"install {args} {packageFile}");
        }
    }
}
