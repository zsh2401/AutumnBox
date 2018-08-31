/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 0:15:54 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.Util;
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
        public static void PushTo(this FileInfo fileInfo, DeviceBasicInfo device, string path)
        {
            ThrowIf.IsNullArg(fileInfo);
            ThrowIf.IsNullArg(path);
            var builder = new AdbCommandBuilder();
            builder.Device(device.Serial.ToString())
                .Arg("push")
                .ArgWithDoubleQuotation(fileInfo.FullName)
                .ArgWithDoubleQuotation(path);
            using (var command = builder.ToCommand())
            {
                var result = command.Execute();
                if (result.ExitCode != 0)
                {
                    throw new AdbCommandFailedException(result.Output);
                }
            }
        }
        /// <summary>
        /// 安装应用
        /// </summary>
        /// <param name="apkFileInfo"></param>
        /// <param name="device"></param>
        public static void InstallTo(this FileInfo apkFileInfo, DeviceBasicInfo device)
        {
            if (apkFileInfo.Extension != ".apk")
            {
                throw new ArgumentException("Is not apk file!", nameof(apkFileInfo));
            }
            var builder = new AdbCommandBuilder();
            builder.Device(device.Serial.ToString())
                .Arg("install")
                .ArgWithDoubleQuotation(apkFileInfo.FullName);
            using (var command = builder.ToCommand())
            {
                var result = command.Execute();
                if (result.ExitCode != 0)
                {
                    throw new AdbCommandFailedException(result.Output);
                }
            }
        }
    }
}
