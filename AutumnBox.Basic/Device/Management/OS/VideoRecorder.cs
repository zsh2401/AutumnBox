/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:25:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;
using AutumnBox.Basic.Util.Debugging;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 视频录制器
    /// </summary>
    public class VideoRecorder : DeviceCommander, Data.IReceiveOutputByTo<VideoRecorder>
    {
        public int Seconds { get; set; } = 180;
        public int BitRate { get; set; } = 4 * 1000 * 1000;
        public Size Size { get; set; } = new Size
        {
            Height = 1280,
            Width = 720
        };
        public bool EnableVerbose { get; set; } = false;
        public string TmpFile { get; set; } = "/sdcard/atmb_record.mp4";
        /// <summary>
        /// 创建新的视频录制器实例
        /// </summary>
        /// <param name="device"></param>
        public VideoRecorder(IDevice device) : base(device)
        {
            ShellCommandHelper.CommandExistsCheck(device, "screenrecord");
        }
        private ShellCommand executingCommand;
        /// <summary>
        /// 开始录制
        /// </summary>
        public void Start()
        {
            Logger<VideoRecorder> logger = new Logger<VideoRecorder>();
            string command = $"screenrecord " +
                $"--size {Size.Width}x{Size.Height} " +
                $"--bit-rate {BitRate} " +
                $"--time-limit {Seconds} ";
            if (EnableVerbose)
            {
                command += "--verbose ";
            }
            command += TmpFile;
            logger.Info("The command of recoding:" + command);
            var executingCommand = CmdStation.GetShellCommand(Device, command);
            executingCommand
                .To(RaiseOutput)
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 停止录制
        /// </summary>
        public void Stop()
        {
            executingCommand.Kill();
        }
        /// <summary>
        /// 将录制完成的文件保存到PC
        /// </summary>
        /// <param name="saveFile"></param>
        public void SaveToPC(FileInfo saveFile)
        {
            CmdStation.GetAdbCommand(Device, $"pull {TmpFile} {saveFile.FullName}")
             .To(RaiseOutput)
             .Execute().
             ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 通过To模式订阅输出事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public VideoRecorder To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
