/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 0:15:54 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using System;
using System.IO;
namespace AutumnBox.Basic.Extension
{
    /// <summary>
    /// 文件信息拓展
    /// </summary>
    public static class FileInfoExtension
    {
        /// <summary>
        /// 推送文件到设备
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="device"></param>
        /// <param name="path"></param>
        /// <param name="cmdStation"></param>
        public static void PushTo(this FileInfo fileInfo, IDevice device, string path, CommandStation cmdStation = null)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException(nameof(fileInfo));
            }

            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            using (var command = cmdStation.NullCheckAndGet()
                .GetAdbCommand(device, $"push \"{fileInfo.FullName}\" \"{path}\""))
            {
                var result = command.Execute();
                if (result.ExitCode != 0)
                {
                    throw new AdbCommandFailedException(result.Output, result.ExitCode);
                }
            }
        }
        /// <summary>
        /// 安装应用
        /// </summary>
        /// <param name="apkFileInfo"></param>
        /// <param name="device"></param>
        /// <param name="cmdStation"></param>
        public static void InstallTo(this FileInfo apkFileInfo, IDevice device, CommandStation cmdStation = null)
        {
            if (apkFileInfo == null)
            {
                throw new ArgumentNullException(nameof(apkFileInfo));
            }

            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }
            if (apkFileInfo.Extension != ".apk")
            {
                throw new ArgumentException("Is not apk file!", nameof(apkFileInfo));
            }
            using (var command = cmdStation.NullCheckAndGet()
                .GetAdbCommand(device, $"install \"{apkFileInfo.FullName}\""))
            {
                var result = command.Execute();
                if (result.ExitCode != 0)
                {
                    throw new AdbCommandFailedException(result.Output, result.ExitCode);
                }
            }
        }
    }
}
