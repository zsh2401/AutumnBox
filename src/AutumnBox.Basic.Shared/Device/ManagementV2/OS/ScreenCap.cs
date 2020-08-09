using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 基于screencap指令的截图器
    /// </summary>
    public sealed class ScreenCap
    {
        private readonly IDevice device;
        private readonly ICommandExecutor executor;
        private readonly string tmpDir;
        /// <summary>
        /// 构造一个截图器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        /// <param name="tempDirectory"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        public ScreenCap(IDevice device, ICommandExecutor executor, string tempDirectory = null)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
            this.tmpDir = tempDirectory ?? ".";
        }
        /// <summary>
        /// 截图
        /// </summary>
        /// <exception cref="CommandErrorException">命令执行失败</exception>
        /// <exception cref="CommandCancelledException">命令被外部进程终止</exception>
        /// <returns>保存到PC上的截图文件名</returns>
        public FileInfo Cap(bool wakeUpDevice = true)
        {
            if (wakeUpDevice)
            {
                new KeyInputer(device, executor).RaiseKeyEvent(AndroidKeyCode.WakeUp);
            }
            string fileName = GenerateUniqueFileName();
            string pcFileName = Path.Combine(tmpDir, fileName);
            executor.AdbShell(device, $"screencap -p /sdcard/{fileName}").ThrowIfError();
            executor.Adb(device, $"pull /sdcard/{fileName} {pcFileName}").ThrowIfError();
            executor.AdbShell(device, $"rm /sdcard/{fileName}");
            return new FileInfo(pcFileName);
        }
        private static string GenerateUniqueFileName()
        {
            return $"atmb_screencap{Guid.NewGuid()}.png";
        }
    }
}
