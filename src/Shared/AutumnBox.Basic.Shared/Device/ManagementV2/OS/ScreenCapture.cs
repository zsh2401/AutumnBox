using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 截图器
    /// </summary>
    public sealed class ScreenCapture
    {
        private const string RANDOM_FILENAME_FMT = "autumnbox_tmp_{0}.png";
        private const string TMP_DIR_ON_DEVICE = "/sdcard/";
        private readonly IDevice device;
        private readonly ICommandExecutor executor;

        /// <summary>
        /// 构造一个截图器
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        public ScreenCapture(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
        /// <summary>
        /// 捕获,并将其作为流打开
        /// </summary>
        /// <returns></returns>
        public Stream CaptureAndOpen()
        {
            var path = GenerateTmpFilePathOnPC();
            CaptureAndSaveToFile(path);
            return new FileStream(path, FileMode.Open, FileAccess.Read);
        }
        /// <summary>
        /// 捕获,并保存到电脑指定文件夹
        /// </summary>
        /// <exception cref="CommandErrorException">命令执行失败</exception>
        /// <param name="savePath"></param>
        public void CaptureAndSaveToFile(string savePath)
        {
            var tmpPath = GenerateTmpFilePath();
            executor.AdbShell(device, $"screencap -p  {tmpPath}").ThrowIfError();
            executor.Adb(device, $"pull {tmpPath} \"{savePath}\"").ThrowIfError();
            executor.AdbShell(device, $"rm  {tmpPath}").ThrowIfError();
        }

        private string GenerateTmpFilePath()
        {
            var fileName = string.Format(RANDOM_FILENAME_FMT, new Random().Next());
            return Path.Combine(TMP_DIR_ON_DEVICE, fileName);
        }

        private string GenerateTmpFilePathOnPC()
        {
            return string.Format(RANDOM_FILENAME_FMT, new Random().Next());
        }
    }
}
