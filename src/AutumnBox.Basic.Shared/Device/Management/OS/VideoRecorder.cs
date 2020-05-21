/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:25:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.IO;
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;
using AutumnBox.Logging;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 视频录制器
    /// </summary>
    [Obsolete("等待重新实现", true)]
    public class VideoRecorder : DeviceCommander
    {
        /// <summary>
        /// 录制时长
        /// </summary>
        public int Seconds { get; set; } = 180;
        /// <summary>
        /// 码率
        /// </summary>
        public int BitRate { get; set; } = 4 * 1000 * 1000;
        /// <summary>
        /// 录制分辨率
        /// </summary>
        public Size Size { get; set; } = new Size
        {
            Height = 1280,
            Width = 720
        };

        /// <summary>
        /// 开启详细信息
        /// </summary>
        public bool EnableVerbose { get; set; } = true;
        /// <summary>
        /// 临时文件存放目录
        /// </summary>
        public string TmpFile { get; set; } = "/sdcard/atmb_record.mp4";
        /// <summary>
        /// 创建新的视频录制器实例
        /// </summary>
        /// <param name="device"></param>
        public VideoRecorder(IDevice device) : base(device)
        {
            ShellCommandHelper.CommandExistsCheck(device, "screenrecord");
        }
        /// <summary>
        /// 开始录制
        /// </summary>
        public void Start()
        {
            throw new NotImplementedException();
            //ILogger logger = LoggerFactory.Auto<VideoRecorder>();
            //string command = $"screenrecord " +
            //    $"--size {Size.Width}x{Size.Height} " +
            //    $"--bit-rate {BitRate} " +
            //    $"--time-limit {Seconds} ";
            //if (EnableVerbose)
            //{
            //    command += "--verbose ";
            //}
            //command += TmpFile;
            //logger.Info("The command of recoding:" + command);
            //var cmd = new RealtimeShellCommand(Device, command);
            //CmdStation.Register(cmd);
            //cmd
            //    .To(RaiseOutput)
            //    .Execute()
            //    .ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 停止录制
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
            //CmdStation.Free();
        }
        /// <summary>
        /// 将录制完成的文件保存到PC
        /// </summary>
        /// <param name="saveFile"></param>
        public void SaveToPC(FileInfo saveFile)
        {
            Executor.Adb(Device, $"pull {TmpFile} {saveFile.FullName}").
              ThrowIfExitCodeNotEqualsZero();
        }
    }
}
